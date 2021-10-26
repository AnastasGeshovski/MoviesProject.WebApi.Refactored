//DOM Elements
let titleInput = document.getElementById("title");
let yearInput = document.getElementById("year");
let genreInput = document.getElementById("genre");
let descriptionInput = document.getElementById("description");
let createMovieBtn = document.getElementById("createMovieBtn");

createMovieBtn.addEventListener("click", function () {
  let movie = {
    Title: titleInput.value,
    Year: yearInput.value,
    Genre: genreInput.value,
    Description: descriptionInput.value,
    CreateMovie: createMovieBtn.value,
  };

  var options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  };

  fetch("https://localhost:44327/api/movie/createmovie", options)
    .then((response) => response.json())
    .then((data) => console.log(data))
    .catch((err) => {
      console.log("Error has occured!!!");
      console.warn(err);
    });
});
