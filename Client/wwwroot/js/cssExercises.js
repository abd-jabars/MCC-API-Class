function testClick1() {
    var data = document.querySelector("div.sct1");
    data.style.backgroundColor = "dodgerblue";
}

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                        <td>${key + 1}</td>
                        <td>${val.name}</td>
                        <td><button data-toggle="modal" data-target="#getPoke" class="btn btn-primary" onclick="getDetails('${val.url}')"> Details </button></td>
                    </tr>`;
    });
    $(".tabelPoke").html(text);
}).fail((error) => {
    console.log(error);
});

function typeColor(val) {
    if (val == "normal") {
        var color = `<h6 class="bg-light text-dark rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "fighting" || val == "dragon" || val == "fire" || val == "psychic") {
        var color = `<h6 class="bg-danger text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "ground" || val == "rock") {
        var color = `<h6 class="bg-secondary text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "ice" || val == "water") {
        var color = `<h6 class="bg-primary text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "dark" || val == "unknown" || val == "shadow" || val == "ghost" || val == "fairy") {
        var color = `<h6 class="bg-dark text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "grass" || val == "poison") {
        var color = `<h6 class="bg-success text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "electric" || val == "flying" || val == "steel") {
        var color = `<h6 class="bg-warning text-dark rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "bug") {
        var color = `<h6 class="bg-info text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    }
}

function getDetails(link) {
    $.ajax({
        url: link
    }).done((result) => {
        console.log(result);

        var pokeName = "";
        pokeName += `<h3 class = "text-capitalize text-center m-auto">Details: ${result.name}</h3>`;
        $(".modal-header").html(pokeName);

        var pokeImage = "";
        pokeImage += `<img class="pokeImage" src="${result.sprites.other.dream_world.front_default}">`;
        $(".pokeImage").html(pokeImage);

        var pokeStats = "";
        $.each(result.stats, function (index) {
            pokeStats += `<tr>
                            <td class = "text-capitalize">${result.stats[index].stat.name}</td>
                            <td>${result.stats[index].base_stat}</td>
                        </tr>`;
            });
        $(".pokeStats").html(pokeStats);

        var pokeType = "";
        $.each(result.types, function (key, val) {
            pokeType += typeColor(val.type.name);
        });
        $(".pokeType").html(pokeType);

        //var pokeType = "";
        //$.each(result.types, function (index) {
        //    pokeType += `<h6 class = "text-capitalize mt-3">${result.types[index].type.name} </h6>`;
        //});
        //$(".pokeType").html(pokeType);

        var pokeAbility = "";
        $.each(result.abilities, function (index) {
            pokeAbility += `<h6 class = "text-capitalize mt-3">${result.abilities[index].ability.name} </h6>`;
        });
        $(".pokeAbility").html(pokeAbility);

        //var pokeMove = "";
        //$.each(result.moves, function (index) {
        //    pokeMove += `<span class = "badge badge-pill badge-light m-1 text-capitalize bg-secondary rounded-sm">${result.moves[index].move.name} </span>`;
        //});
        //$(".pokeMove").html(pokeMove);

    }).fail((error) => {
        console.log(error);
    });
}

        //var pokemon = "";
        //pokemon += `<div class="row mt-20">
        //                <div class="col">
        //                    <div class="text-center">
        //                        <img class="pokeImage" src="${result.sprites.other.dream_world.front_default}">
        //                    </div>
        //                </div>
        //                <div class="col">
        //                    <h3 class="modal-title text-capitalize text-center" id="exampleModalLabel">${result.name}</h3>
        //                    <div class="row pt-4">
        //                        <div class="col mt-20">
        //                            <h5 class="text-capitalize text-left ml-15" id="exampleModalLabel">Type</h5>
        //                        </div>
        //                        <div class="col">
        //                                <span class="text-capitalize">${tipe += typeColor(result.types[0].type.name)}</span>
        //                        </div>
        //                    </div>
        //                    <div class="row pt-4">
        //                        <div class="col">
        //                            <h5 class="text-capitalize text-left" id="exampleModalLabel">Ability</h5>
        //                        </div>
        //                        <div class="col text-left">
        //                            <ul class="list-inline">
        //                                <li class="text-capitalize list-inline-item">${result.abilities[0].ability.name}</li>
        //                                <li class="text-capitalize list-inline-item">${result.abilities[1].ability.name}</li>
        //                            </ul>
        //                        </div>
        //                    </div>
        //                </div>
        //            </div>
        //            <div class="row">
        //                    <h5 class="text-capitalize text-center m-auto" id="exampleModalLabel">Stats</h5>
        //            </div>
        //            <div class="row">
        //                <div class="col border-top pt-4">
        //                    <p class="text-center">Height: ${result.height} m</p>
        //                    <p class="text-capitalize text-center">Weight: ${result.weight} kg</p>
        //                </div>
        //                <div class="col">
        //                    <div class="progress">
        //                      <div class="progress-bar" role="progressbar" aria-valuenow="70"
        //                      aria-valuemin="0" aria-valuemax="100" style="width:70%">
        //                        70%
        //                      </div>
        //                    </div>
        //                </div>
        //                <div class="col border-top pt-4">
        //                    <p class="text-capitalize text-center">${result.stats[0].stat.name}: ${result.stats[0].base_stat}</p>
        //                    <p class="text-capitalize text-center">${result.stats[5].stat.name}: ${result.stats[5].base_stat}</p>
        //                </div>
        //                <div class="col border-top pt-4">
        //                    <p class="text-capitalize text-center">${result.stats[1].stat.name}: ${result.stats[1].base_stat}</p>
        //                    <p class="text-capitalize text-center">${result.stats[2].stat.name}: ${result.stats[2].base_stat}</p>
        //                </div>
        //                <div class="col border-top pt-4">
        //                    <p class="text-capitalize text-center">${result.stats[3].stat.name}: ${result.stats[3].base_stat}</p>
        //                    <p class="text-capitalize text-center">${result.stats[4].stat.name}: ${result.stats[4].base_stat}</p>
        //                </div>
        //            </div>`
        //$(".modal-body").html(pokemon);

        //<div class="row pt-4">
        //    <div class="col">
        //        <h5 class="text-capitalize text-left" id="exampleModalLabel">Body</h5>
        //    </div>
        //    <div class="col text-left">
        //        <p class="text-center">Height: ${result.height} m</p>
        //        <p class="text-capitalize text-center">Weight: ${result.weight} kg</p>
        //    </div>
        //</div>
        //var pokemon = "";
        //pokemon += `<div class="row mt-20">
        //                <div class="col">
        //                    <h3 class="modal-title text-capitalize text-center" id="exampleModalLabel">${result.name}</h3>
        //                    <div class="text-center">
        //                        <img class="pokeImage" src="${result.sprites.other.dream_world.front_default}">
        //                    </div>
        //                </div>
        //                <div class="col">
        //                    <div class="row">
        //                        <div class="col">
        //                            <h5 class="text-capitalize text-left" id="exampleModalLabel">Type</h5>
        //                        </div>
        //                        <div class="col">
        //                                <span class="text-capitalize">${result.types[0].type.name}</span>
        //                        </div>
        //                    </div>
        //                    <div class="row">
        //                        <div class="col">
        //                            <h5 class="text-capitalize text-left" id="exampleModalLabel">Ability</h5>
        //                        </div>
        //                        <div class="col text-left">
        //                            <ul>
        //                                <li class="text-capitalize">${result.abilities[0].ability.name}</li>
        //                                <li class="text-capitalize">${result.abilities[1].ability.name}</li>
        //                            </ul>
        //                        </div>
        //                    </div>
        //                    <div class="row">
        //                        <div class="col">
        //                            <h5 class="text-capitalize text-left" id="exampleModalLabel">State</h5>
        //                        </div>
        //                        <div class="col">
        //                            <p class="text-capitalize">${result.stats[0].stat.name}: ${result.stats[0].base_stat}</p>
        //                            <p class="text-capitalize">${result.stats[1].stat.name}: ${result.stats[1].base_stat}</p>
        //                            <p class="text-capitalize">${result.stats[2].stat.name}: ${result.stats[2].base_stat}</p>
        //                            <p class="text-capitalize">${result.stats[3].stat.name}: ${result.stats[3].base_stat}</p>
        //                            <p class="text-capitalize">${result.stats[4].stat.name}: ${result.stats[4].base_stat}</p>
        //                            <p class="text-capitalize">${result.stats[5].stat.name}: ${result.stats[5].base_stat}</p>
        //                        </div>
        //                    </div>
        //                </div>
        //            </div>`
        //$(".modal-body").html(pokemon);

        //var pokeImage = ``
        //$(".modal-body").html(pokeImage);
        //<ul>
        //    <li class="text-capitalize">${result.abilities[0].ability.name}</li>
        //    <li class="text-capitalize">${result.abilities[1].ability.name}</li>
        //</ul>
        //<p class="text-capitalize">${result.abilities[0].ability.name}</p>
        //<p class="text-capitalize">${result.abilities[1].ability.name}</p>
    