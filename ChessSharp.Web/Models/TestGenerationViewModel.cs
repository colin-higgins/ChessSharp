using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessSharp.Web.Models
{
    public class TestGenerationViewModel
    {
        [Required]
        public string TestName { get; set; }

        public string ParentTestName { get; set; }

        [Required]
        public string GameState { get; set; }

        [Required]
        public string TestMove { get; set; }

        public bool IsLegal { get; set; }

        public string Message { get; set; }
    }

    public class IndividualTestResultViewModel
    {
        public string TestName { get; set; }

        public bool ExpectedLegality { get; set; }

        public bool ActualLegality { get; set; }

        public bool TestPassed { get; set; }

        public string GameState { get; set; }
        public string TestMove { get; set; }
    }

    public class TestResultsViewModel
    {
        public TestResultsViewModel()
        {
            Results = new List<IndividualTestResultViewModel>();
        }

        public List<IndividualTestResultViewModel> Results { get; set; }
    }
}