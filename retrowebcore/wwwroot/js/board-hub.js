"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/boardhub").build();

//Disable send button until connection is established
document.getElementById("addNewBoard").disabled = true;

const SQUAD_TEMPLATE = '<[[SQUAD_NAME]]>';
const SPRINT_TEMPLATE = '<[[SPRINT_NAME]]>';

var TEMPLATE = `
        <div class="col-3 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title"> <[[SQUAD_NAME]]> </h5>
                    <p class="card-text">
                        <[[SPRINT_NAME]]>
                    </p>
                    <a href="#" class="btn btn-primary">Go to Board</a>
                </div>
            </div>
        </div>`;

connection.on("hubNewBoardEvent", function (squad, sprint) {
    $('#sendButtonSpinner').addClass("hidden");
    var newBoard = TEMPLATE.replace(SQUAD_TEMPLATE, squad).replace(SPRINT_TEMPLATE, sprint)
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
