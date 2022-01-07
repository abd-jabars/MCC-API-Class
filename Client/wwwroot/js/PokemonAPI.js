$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    var text = "";
    $.each(result.results, function (key, val) {
        text += `<tr>
                        <td>${key + 1}</td>
                        <td class = "text-capitalize">${val.name}</td>
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
    } else if (val == "ground" || val == "rock" || val == "flying") {
        var color = `<h6 class="bg-secondary text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "ice" || val == "water") {
        var color = `<h6 class="bg-primary text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "dark" || val == "unknown" || val == "shadow" || val == "ghost" || val == "fairy") {
        var color = `<h6 class="bg-dark text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "grass") {
        var color = `<h6 class="bg-success text-white rounded-sm d-inline p-1 mr-1">${val}</h6>`;
        return color;
    } else if (val == "electric" || val == "steel" || val == "poison") {
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
        console.log(link);

        var pokeName = "";
        pokeName += `<h3 class = "text-capitalize text-center m-auto">Details: ${result.name}</h3>`;
        $(".modal-header").html(pokeName);

        var pokeImage = "";
        pokeImage += `<img class="pokeImage" src="${result.sprites.other.dream_world.front_default}">`;
        $(".pokeImage").html(pokeImage);

        var statNameValue = "";
        $.each(result.stats, function (index) {
            statNameValue += `<div class="row mt-3">
                                  <div class="col d-inline font-weight-bold text-left text-capitalize medium"> ${result.stats[index].stat.name} </div>
                                  <div class="col d-inline font-weight-bold text-right text-capitalize medium"> ${result.stats[index].base_stat} </div>
                              </div>
                              <div class="row progress mb-3 mx-auto">
                                  <div class="progress-bar" role="progressbar" style="width: ${result.stats[index].base_stat}%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                              </div>`;
            });
        $(".statNameValue").html(statNameValue);

        var pokeType = "";
        $.each(result.types, function (key, val) {
            pokeType += typeColor(val.type.name);
        });
        $(".pokeType").html(pokeType);

        var pokeAbility = "";
        $.each(result.abilities, function (index) {
            pokeAbility += `<h6 class = "text-capitalize mt-3">${result.abilities[index].ability.name} </h6>`;
        });
        $(".pokeAbility").html(pokeAbility);

    }).fail((error) => {
        console.log(error);
    });
}
   