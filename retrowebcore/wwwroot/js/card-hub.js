"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cardhub").build();

//Disable send button until connection is established
document.getElementById("likedLaneAddChild").disabled = true;
document.getElementById("lackedLaneAddChild").disabled = true;
document.getElementById("learnedLaneAddChild").disabled = true;
document.getElementById("longedforLaneAddChild").disabled = true;

const CARD_ID_TEMPLATE = '<[[CARD_ID_TEMPLATE]]>';
const CARD_CONTENT_TEMPLATE = '<[[CARD_CONTENT_TEMPLATE]]>';
const SLUG_TEMPLATE = '<[[SLUG]]>';

var TEMPLATE = `
        <div class="card draggable shadow-sm dropzone" 
            id="${CARD_ID_TEMPLATE}" draggable="true" 
            ondragstart="drag(event)"
            ondrop="merge(event)" 
            ondragover="allowDrop(event)" 
            ondragleave="clearDrop(event)"
        >
            <div class="card-body p-1">
                <p>
                    ${CARD_CONTENT_TEMPLATE}
                </p>
                <button class="btn btn-primary btn-sm">View</button>
            </div>
        </div>
        <div class="dropzone rounded" ondrop="drop(event)" ondragover="allowDrop(event)" ondragleave="clearDrop(event)"> &nbsp; </div>`;

connection.on("hubNewCardLikedEvent", function (responseJson) {
    var response = JSON.parse(responseJson);
    var newBoard = TEMPLATE
        .replace(CARD_ID_TEMPLATE, response.squad)
        .replace(CARD_CONTENT_TEMPLATE, response.sprint)
    $('#likedLaneAddChildSpinner').addClass("hidden");
    $('#likedLane').append(newBoard);
});

connection.start().then(function () {
    document.getElementById("likedLaneAddChild").disabled = false;
    document.getElementById("lackedLaneAddChild").disabled = false;
    document.getElementById("learnedLaneAddChild").disabled = false;
    document.getElementById("longedforLaneAddChild").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

$("#likedLaneAddChild").click(function (event) {
    $('#likedLaneAddChildSpinner').removeClass("hidden");
    var boardslug = $("#board-id").attr('data-name');
    connection.invoke("hubAddNewCard", boardslug, 1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
