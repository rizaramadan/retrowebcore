"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/boardhub").build();

//Disable send button until connection is established
document.getElementById("addNewBoard").disabled = true;

const SQUAD_TEMPLATE = '<[[SQUAD_NAME]]>';
const SPRINT_TEMPLATE = '<[[SPRINT_NAME]]>';
const SLUG_TEMPLATE = '<[[SLUG]]>';

var TEMPLATE = `
        <div class="col-3 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title"> ${SQUAD_TEMPLATE} </h5>
                    <p class="card-text">
                        ${SPRINT_TEMPLATE}
                    </p>
                    <a href="/board/view/${SLUG_TEMPLATE}" class="btn btn-primary">Go to Board</a>
                    <a href="/board/archive/${SLUG_TEMPLATE}" class="btn btn-warning">Archive</a>
                </div>
            </div>
        </div>`;

connection.on("hubNewBoardEvent", function (responseJson) {
    var response = JSON.parse(responseJson);
    var newBoard = TEMPLATE
        .replace(SQUAD_TEMPLATE, response.squad)
        .replace(SPRINT_TEMPLATE, response.sprint)
        .replace(SLUG_TEMPLATE, response.slug)
        .replace(SLUG_TEMPLATE, response.slug);
    $('#sendButtonSpinner').addClass("hidden");
    $('#boardContainer').append(newBoard);
});

connection.start().then(function () {
    document.getElementById("addNewBoard").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

$("#addNewBoard").click(function (event) {
    $('#sendButtonSpinner').removeClass("hidden");
    var squad = document.getElementById("squadName").value;
    var sprint = document.getElementById("sprintName").value;
    connection.invoke("hubAddNewBoard", squad, sprint).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
