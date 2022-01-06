// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var tes = document.getElementById("tes");
tes.style.backgroundColor = "sandybrown";
tes.innerHTML = "Belajar CSS-(selector) Javascript jQuery";
tes.style.textAlign = "center";

//var tes01 = document.getElementById("h2#tes h2"); // didnt work

//var tes02 = document.getElementsByClassName("li.list2"); // didnt work

var list = document.getElementsByClassName("list");
list[0].style.color = "salmon";
list[1].style.color = "orange";
list[2].style.color = "lightgreen";

var tes1 = $("li.list a.link");
tes1[1].innerHTML = "Test jQuery 1.1";

$(".list:nth-child(2n+1) a").html("Test jQuery 1.2");

var tes2 = $("li.list2 a.link").html("Test jQuery 2");

var tes3 = document.querySelector("li.list a");
var tes4 = document.querySelectorAll("li.list a");

var testClick1 = document.getElementsByClassName("testClick1");
testClick1[0].addEventListener("click", function () {
    testClick1[0].innerHTML = "U did it";
})

$("div.testClick2").on("click", function () {
    $("div.testClick3").html("Pls click me!!!!!!!!!!!!!");
})

$("div.testClick3").on("click", function () {
    $("div.testClick2").html("Im sorry line 3 :')");
})

$("div.testClick4").on("click", function () {
    $("div.testClick1").html("Click to change this line");
    $("div.testClick2").html("Click to change next line");
    $("div.testClick3").html("Click to change previous line");
    $("div.testClick5").html("Try to click me");
})

$(".testHover1").hover(
    function () {
        $(this).css('color', 'red');
        $(this).html("how dare u!!!");
        $('[data-toggle="tooltip"]').tooltip();
    },
    function() {
        $(this).css('color', 'black');
        $(this).html("Don't touch me");
    }
);

$(".testHover1")

function clickMe() {
    $("div.testClick5").html("voila");
}

const animals = [
    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
]

console.log(animals);

//let OnlyCat = new Array();
let onlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == 'cat') {
        onlyCat.push(animals[i]);
    }
    //else if (animals[i].species == 'snail') {
    //else {
    //    animals[i].kelas.name = 'non-mamalia';
    //}
}

console.log(onlyCat);
//console.log(animals);

//function CatOnly() {
//    let OnlyCat = [];

//    for (var i = 0; i < animals.length; i++) {
//        if (animals[i].species == 'cat') {
//            OnlyCat.push(animals[i]);
//        }
//    }

//    console.log(OnlyCat);
//}

//let newAnimal = new Array();
//newAnimal = animals;
//for (var i = 0; i < animals.length; i++) {
//    if (newAnimal[i].species == 'snail') {
//        newAnimal[i].kelas.name = 'non-mamalia';
//    }
//}

//console.log(newAnimal);

function ClassName() {
    //let newAnimal = animals // didnt work, animals class name changed too
    let newAnimal = new Array();
    newAnimal = animals;
    for (var i = 0; i < animals.length; i++) {
        if (newAnimal[i].species == 'snail') {
            newAnimal[i].kelas.name = 'non-mamalia';
        }
    }

    console.log(newAnimal);
}

function ClassName1() {
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == 'snail') {
            animals[i].kelas.name = 'non-mamalia';
        }
    }

    console.log(animals);
}

//let arr = ['Apple', { name: 'John' }, true, function () { alert('hello'); }];

//console.log(arr);