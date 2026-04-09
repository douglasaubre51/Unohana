"use strict"

var conn = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notice-hub")
    .build()


conn.on("UpdateMessage", (dto) => {
    console.log(`${dto.userId} says: ${dto.text}`)

    let messageContain = document.getElementById("message-contain")
    let newMessageCard = document.createElement("div")
    newMessageCard.setAttribute('class', 'message-card')

    let cardText = document.createElement("span")
    cardText.setAttribute("class", "message-text")
    cardText.textContent = dto.text
    newMessageCard.appendChild(cardText)

    let cardTime = document.createElement("span")
    cardTime.setAttribute("class", "message-time")
    cardTime.textContent = dto.createdAt
    newMessageCard.appendChild(cardTime)

    let cardAuthor = document.createElement("span")
    cardAuthor.setAttribute("class", "message-author")
    cardAuthor.textContent = dto.username
    newMessageCard.appendChild(cardAuthor)

    messageContain.appendChild(newMessageCard)
})


// Start signalR connection!
conn.start()
    .then(() => {
        console.log(`signalR initializing!`)

        let userId = document.getElementById("user-id").value
        let channelId = document.getElementById("channel-id").value

        conn.invoke("SetUserId", { userId: userId, channelId: channelId })
    })
    .catch((err) => {
        return console.error(err.toString())
    })
