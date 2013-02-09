function displayboard() {

        var elemID = ""
        var square = document.getElementById("square00")
        for (i = 0; i <= 63; i++) {
            if (i < 10) {
                elemID = "square0" + i;
            }
            else {
                elemID = "square" + i;
            }
            square = document.getElementByID(elemID);

            //all rows start with even number
            if (i == 0) {
                square.setAttribute("class", "square blacksquare");
            }
            else if (((i/8) == 0) || ((i/8)%2 == 0)) {
                //row starts black
                if (i % 2 == 0) {
                    square.setAttribute("class", "square blacksquare");
                }
                else {
                    square.setAttribute("class", "square whitesquare");
                }
            }
            else {
                //row starts white
                if (i % 2 == 0) {
                    square.setAttribute("class", "square whitesquare");
                }
                else {
                    square.setAttribute("class", "square blacksquare");
                }
            }

        }
 //   } // else if color black


}  //end function displayboard