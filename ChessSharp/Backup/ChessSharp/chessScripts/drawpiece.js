function drawImage(squareName, imagePath) {

var pieceImage = document.createElement("IMG");

pieceImage.src = imagePath;

document.getElementById(squareName).appendChild(pieceImage);

}  //end function drawpiece