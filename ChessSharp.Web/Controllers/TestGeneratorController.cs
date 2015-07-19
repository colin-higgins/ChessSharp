using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Chess.Data.Entities;
using Chess.Domain;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class TestGeneratorController : BaseController
    {
        private const string MessageKey = "GenerationMessage";

        protected string GenerationMessage
        {
            get { return (string)TempData[MessageKey]; }
            set { TempData[MessageKey] = value; }
        }

        [HttpGet]
        public ActionResult Index(string testKey = null)
        {
            var model = new TestGenerationViewModel
            {
                GameState = NewTestGame()
            };

            if (testKey != null)
            {
                model = RetrieveTestCaseAsTemplate(testKey) ?? model;
                CleanupModelForNextTest(model);
            }

            if (GenerationMessage != null)
            {
                model.Message = GenerationMessage;
            }

            return View(model);
        }

        private TestGenerationViewModel RetrieveTestCaseAsTemplate(string testKey)
        {
            var parent = TryLoadTestCase(testKey);

            if (parent == null)
            {
                return null;
            }

            AdvanceGameAccordingToMove(parent);

            return parent;
        }

        private static void CleanupModelForNextTest(TestGenerationViewModel parent)
        {
            parent.ParentTestName = parent.TestName; //Sets up a hierarchy
            parent.IsLegal = default(bool);

            parent.TestMove = null;
            parent.TestName = null;
        }

        private string GetMessageIfIllegalMove(TestGenerationViewModel model)
        {
            var move = GetMoveFromString(model);

            var gameToUpdate = GetTypedObject<GameModel>(model.GameState);

            var game = Map<Game>(gameToUpdate);

            var gameManager = new GameManager(game);

            try
            {
                gameManager.MovePiece(move);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }

        private void AdvanceGameAccordingToMove(TestGenerationViewModel model)
        {
            if (model.IsLegal)
            {
                var move = GetMoveFromString(model);

                var gameToUpdate = GetTypedObject<GameModel>(model.GameState);

                var game = Map<Game>(gameToUpdate);

                var gameManager = new GameManager(game);

                try
                {
                    gameManager.MovePiece(move);
                }
                catch (Exception ex)
                {
                    GenerationMessage = "The move you submitted was determined invalid by the game manager: " + ex.Message;
                }

                gameToUpdate = Map<GameModel>(game);

                gameToUpdate.Moves.Last().MoveId = gameToUpdate.Moves.Count;

                var board = new Board(game.Squares.ToArray());

                gameToUpdate.Board = Map<BoardViewModel>(board);

                model.GameState = GetJson(gameToUpdate);
            }
        }

        private static Move GetMoveFromString(TestGenerationViewModel model)
        {
            var moveToMake = GetTypedObject<MoveViewModel>(model.TestMove);
            var move = Map<Move>(moveToMake);
            return move;
        }

        private static TDest Map<TDest>(object source)
        {
            return AutoMapper.Mapper.Map<TDest>(source);
        }

        [HttpGet]
        public ActionResult RunTests()
        {
            var model = new TestResultsViewModel();

            var testKeys = GetAllTestKeys();

            foreach (var testKey in testKeys)
            {
                var test = TryLoadTestCase(testKey);

                var illegalMoveMessage = GetMessageIfIllegalMove(test);
                var isLegalMove = illegalMoveMessage == null;

                var result = new IndividualTestResultViewModel()
                {
                    ActualLegality = isLegalMove,
                    ExpectedLegality = test.IsLegal,
                    TestPassed = isLegalMove == test.IsLegal,
                    TestName = test.TestName,
                    TestMove = test.TestMove,
                    InvalidReason = illegalMoveMessage
                    
                };

                if (result.TestPassed == false)
                {
                    result.GameState = test.GameState;
                }

                model.Results.Add(result);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TestGenerationViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                GenerationMessage = "The test case submitted is invalid. It was not created.";
                return RedirectToAction("Index", new { testKey = model.ParentTestName });
            }

            SaveTestCase(model);
            GenerationMessage = "The test case submitted was saved.";

            if (!model.IsLegal)
            {
                GenerationMessage = "The test case submitted was saved. Because it was an illegal move we have reverted to your previous legal move.";
                return RedirectToAction("Index", new { testKey = model.ParentTestName });
            }

            return RedirectToAction("Index", new { testKey = model.TestName });
        }

        private string NewTestGame()
        {
            var board = new Board();
            var squares = board.Squares.SelectMany(s => s).ToList();

            var game = new Game
            {
                DarkPlayer = new ChessUser() { DisplayName = "Dark" },
                LightPlayer = new ChessUser() { DisplayName = "Light" },
                Name = "Test Generation Game",
                Squares = squares,
                Moves = new List<Move>()
            };

            foreach (var square in squares)
            {
                square.Game = game; //Setup parent reference
            }

            var gameModel = Map<GameModel>(game);

            gameModel.Board = Map<BoardViewModel>(board);

            var json = GetJson(gameModel);

            return json;
        }

        private TestGenerationViewModel TryLoadTestCase(string key)
        {
            var testFile = TestPath(key);

            var testCaseExists = System.IO.File.Exists(testFile);

            if (!testCaseExists)
                return null;

            var testCaseJson = System.IO.File.ReadAllText(TestPath(key));

            var model = GetTypedObject<TestGenerationViewModel>(testCaseJson);

            return model;
        }

        private static T GetTypedObject<T>(string testCaseJson)
        {
            var serializer = new JavaScriptSerializer();

            var model = serializer.Deserialize<T>(testCaseJson);
            return model;
        }

        protected string BaseTestPath
        {
            get
            {
                var basePath = HttpRuntime.AppDomainAppPath;

                return basePath + "/TestCases/";
            }
        }

        private string TestPath(string key)
        {
            return BaseTestPath + key + ".json";
        }

        private List<string> GetAllTestKeys()
        {
            var d = new DirectoryInfo(BaseTestPath);
            var files = d.GetFiles("*.json"); 
            var keys = 
                files.Select(file => Path.GetFileNameWithoutExtension(file.Name)).ToList();
            return keys;
        }

        private void SaveTestCase(TestGenerationViewModel model)
        {
            var jsonToStore = GetJson(model);

            System.IO.File.WriteAllText(TestPath(model.TestName), jsonToStore);
        }

        private static string GetJson(object thing)
        {
            var serializer = new JavaScriptSerializer();

            var jsonToStore = serializer.Serialize(thing);

            return jsonToStore;
        }
    }
}
