"use strict";

var OPENAI_API_KEY = "INSERT YOUR API KEY HERE";
var bTextToSpeechSupported = true;
var bSpeechInProgress = true;
var oSpeechRecognizer = null;
var oSpeechSynthesisUtterance = null;
var oVoices = null;
var apiUrl = "https://localhost:7220/api";
var clickedLabel = true;
let category;

///////////////////////////
// Form validation

const password = document.getElementById("signup-pass").value;
const confirmPassword = document.getElementById("signup-passconfirm").value;

const validatePassword = function () {
  if (password !== confirmPassword) {
    confirmPassword.setCustomValidity("Passwords Don't Match");
  }
  confirmPassword.setCustomValidity("");
};

document.querySelectorAll(".auth-panel").forEach((item) => {
  item.addEventListener("click", (event) => {
    event.preventDefault();
    clickedLabel = !clickedLabel;
    let loginForm = document.querySelector(".login");
    let registrationForm = document.querySelector(".signup");
    loginForm.style.display = clickedLabel ? "block" : "none";
    registrationForm.style.display = !clickedLabel ? "block" : "none";
  });
});

// password.onchange = validatePassword;
// confirmPassword.onkeyup = validatePassword;

function Login() {
  let email = document.querySelector("#login-user").value;
  let password = document.querySelector("#login-pass").value;

  let params = {
    email: email,
    password: password,
  };

  var oHttp = new XMLHttpRequest();
  oHttp.open("POST", `${apiUrl}/Auth/Login`);
  oHttp.setRequestHeader("Accept", "application/json");
  oHttp.setRequestHeader("Content-Type", "application/json");

  oHttp.send(JSON.stringify(params));

  oHttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
      let response = JSON.parse(oHttp.response);
      localStorage.setItem("token", response.token);
      let loginForm = document.querySelector(".login");
      loginForm.style.display = "none";
      document.querySelector(".settings__profile-name").innerHTML =
        response.userName;
      document.querySelector(".image-container").style.display = "none";
      document.querySelector(".main-container").style.display = "flex";
    }
  };
}

function Register() {
  let userName = document.querySelector("#signup-user").value;
  let email = document.querySelector("#signup-email").value;
  let password = document.querySelector("#signup-pass").value;

  let params = {
    userName: userName,
    email: email,
    password: password,
  };

  var oHttp = new XMLHttpRequest();
  oHttp.open("POST", `${apiUrl}/Auth/Register`);
  oHttp.setRequestHeader("Accept", "application/json");
  oHttp.setRequestHeader("Content-Type", "application/json");

  oHttp.send(JSON.stringify(params));

  oHttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
      let loginForm = document.querySelector(".login");
      let registrationForm = document.querySelector(".signup");
      loginForm.style.display = "block";
      registrationForm.style.display = "none";
    }
  };
}

document.querySelector(".logout-button").addEventListener("click", (event) => {
  LogOut();
});

document.querySelector(".login__button").addEventListener("click", (event) => {
  event.preventDefault();
  Login();
});

document.querySelector(".signup__button").addEventListener("click", (event) => {
  event.preventDefault();
  Register();
});

document
  .querySelector(".character__form-btn")
  .addEventListener("click", (event) => {
    event.preventDefault();
    CreateStory(category);
  });

function LogOut() {
  const loginForm = document.querySelector(".login");
  const welcomeScreen = document.querySelector(".welcome");

  localStorage.removeItem("token");

  loginForm.style.display = "block";
  welcomeScreen.style.display = "block";

  document.querySelector(".settings__profile-name").innerHTML = "";
  clearFields();
  hideContainers();
}

function GeneratePlayer() {
  let worldTypeText;
  let raceText;
  let classTypeText;

  let value = document.getElementsByName("world");
  for (var radio of value) {
    if (radio.checked) {
      worldTypeText = radio.id;
    }
  }

  value = document.getElementsByName("race");
  for (var radio of value) {
    if (radio.checked) {
      raceText = radio.id;
    }
  }

  value = document.getElementsByName("class");
  for (var radio of value) {
    if (radio.checked) {
      classTypeText = radio.id;
    }
  }

  let worldType = getWorldType(worldTypeText);
  let race = getRace(raceText);
  let classType = getClassType(classTypeText);

  return [worldType, race, classType];
}

function getWorldType(worldType) {
  switch (worldType) {
    case "azeroth":
      return 0;
    case "faerun":
      return 1;
    case "westeros":
      return 2;
  }
}

function getRace(race) {
  switch (race) {
    case "human":
      return 0;
    case "elf":
      return 1;
    case "dwarf":
      return 2;
    case "gnome":
      return 3;
  }
}

function getClassType(classType) {
  switch (classType) {
    case "wizard":
      return 0;
    case "fighter":
      return 1;
    case "cleric":
      return 2;
    case "barbarian":
      return 3;
    case "paladin":
      return 4;
  }
}

function clearFields() {
  document.querySelector("#login-user").value = "";
  document.querySelector("#login-pass").value = "";
  document.querySelector("#signup-user").value = "";
  document.querySelector("#signup-email").value = "";
  document.querySelector("#signup-pass").value = "";
  document.querySelector("#signup-passconfirm").value = "";
}

///////////////////////////
// Chatbot

document.querySelectorAll(".categories__category").forEach((item) => {
  item.addEventListener("click", (event) => {
    event.preventDefault();
    category = item.id;
    clearChatMessages();
    stopSpeechSynthesis();
    document.querySelector(".character").style.display = "block";
    document.querySelector(".chat").style.display = "none";
    document.querySelector(".welcome").style.display = "none";
  });
});

// When a user starts a new chat, the chat messages should be cleared
function clearChatMessages() {
  const chatContainer = document.querySelector(".messages");
  chatContainer.innerHTML = ""; // This clears all child elements
}

// On a new chat, any speech synthesis should be stopped
function stopSpeechSynthesis() {
  if (window.speechSynthesis) {
    window.speechSynthesis.cancel();
  }
}

function CreateStory(category) {
  let character = GeneratePlayer();

  let params = {
    category: getCategoryNumber(category),
    world: character[0],
    race: character[1],
    class: character[2],
  };

  let token = localStorage.getItem("token");
  var oHttp = new XMLHttpRequest();
  oHttp.open("POST", `${apiUrl}/Stories?token=${token}`);
  oHttp.setRequestHeader("Accept", "application/json");
  oHttp.setRequestHeader("Content-Type", "application/json");
  oHttp.setRequestHeader("Authorization", `Bearer ${token}`);
  oHttp.send(JSON.stringify(params));

  oHttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 201) {
      let response = JSON.parse(oHttp.response);
      localStorage.setItem("active-story", `${response.id}`);
      document.querySelector(".character").style.display = "none";
      document.querySelector(".chat").style.display = "block";
      document.querySelector(".image-container").style.display = "flex";
    }
  };
}

function AddMessage(message) {
  let params = {
    category: getCategoryNumber(category),
  };

  let token = localStorage.getItem("token");
  var oHttp = new XMLHttpRequest();
  oHttp.open("POST", `${apiUrl}/Stories?token=${token}`);
  oHttp.setRequestHeader("Accept", "application/json");
  oHttp.setRequestHeader("Content-Type", "application/json");
  oHttp.setRequestHeader("Authorization", `Bearer ${token}`);
  oHttp.send(JSON.stringify(params));

  oHttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 201) {
      let response = JSON.parse(oHttp.response);
      localStorage.setItem("active-story", `${response.id}`);
    }
  };
}

function getCategoryNumber(category) {
  switch (category) {
    case "fantasy":
      return 0;
    case "sci-fi":
      return 1;
    case "mystery":
      return 2;
  }
}

function OnLoad() {
  if ("webkitSpeechRecognition" in window) {
  } else {
    //speech to text not supported
    lblSpeak.style.display = "none";
  }

  if ("speechSynthesis" in window) {
    bTextToSpeechSupported = true;

    speechSynthesis.onvoiceschanged = function () {
      oVoices = window.speechSynthesis.getVoices();
      for (var i = 0; i < oVoices.length; i++) {
        selVoices[selVoices.length] = new Option(oVoices[i].name, i);
      }
    };
  }

  if (localStorage.getItem("token") == null) {
    let loginForm = document.querySelector(".login");
    loginForm.style.display = "block";
    hideContainers();
  }
}

function ChangeLang(o) {
  if (oSpeechRecognizer) {
    oSpeechRecognizer.lang = selLang.value;
    //SpeechToText()
  }
}

function hideContainers() {
  document.querySelector(".character").style.display = "none";
  document.querySelector(".chat").style.display = "none";
  document.querySelector(".image-container").style.display = "none";
  document.querySelector(".main-container").style.display = "none";
}

function Send() {
  var sQuestion = txtMsg.value;

  if (sQuestion == "") {
    alert("Type in your question!");
    txtMsg.focus();
    return;
  }

  // Show the user's question first
  renderUserMarkup(sQuestion);
  // Then show the bot's response
  renderBotMarkup("", true);

  var oHttp = new XMLHttpRequest();
  oHttp.open("POST", "https://api.openai.com/v1/chat/completions");
  oHttp.setRequestHeader("Accept", "application/json");
  oHttp.setRequestHeader("Content-Type", "application/json");
  oHttp.setRequestHeader("Authorization", "Bearer " + OPENAI_API_KEY);

  oHttp.onreadystatechange = function () {
    if (oHttp.readyState === 4) {
      //console.log(oHttp.status);
      var oJson = {};
      if (txtOutput.value != "") txtOutput.value += "\n";

      try {
        oJson = JSON.parse(oHttp.responseText);
      } catch (ex) {
        txtOutput.value += "Error: " + ex.message;
      }

      if (oJson.error && oJson.error.message) {
        txtOutput.value += "Error: " + oJson.error.message;
      } else if (oJson.choices && oJson.choices[0].message.content) {
        var s = oJson.choices[0].message.content;

        if (selLang.value != "en-US") {
          var a = s.split("?\n");
          if (a.length == 2) {
            s = a[1];
          }
        }

        if (s == "") s = "No response";
        txtOutput.value += "Chat GPT: " + s;
        var element = document.getElementById("sampleLastMessage");
        element.innerHTML = s;
        var text = element.innerHTML;

        fetch("https://api.openai.com/v1/images/generations", {
          method: "POST",
          body: JSON.stringify({
            model: "dall-e-2",
            prompt: text,
            n: 1,
            size: "256x256",
          }),
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + OPENAI_API_KEY,
          },
        })
          .then((response) => response.json())
          .then((json) => createImage(json))
          .then(() => renderBotMarkup(s))
          .then(() => TextToSpeech(s));

        function createImage(responseData) {
          var image = document.getElementById("image");
          if (
            responseData &&
            responseData.data &&
            responseData.data.length > 0
          ) {
            // Extracting the URL from the response
            var imageUrl = responseData.data[0].url; // Adjust based on actual response structure
            image.src = imageUrl;
          } else {
            console.error(
              "Failed to generate image or extract URL from response."
            );
          }
        }
      }
    }
  };

  var sModel = "gpt-3.5-turbo";
  var iMaxTokens = 2048;
  var sUserId = "1";
  var dTemperature = 0.5;
  var element = document.getElementById("sampleLastMessage").innerHTML;

  var data = {
    model: sModel,
    messages: [
      {
        role: "user",
        content: element + sQuestion,
      },
    ],
    max_tokens: iMaxTokens,
    user: sUserId,
    temperature: dTemperature,
    frequency_penalty: 0.0, //Number between -2.0 and 2.0
    //Positive values decrease the model's likelihood
    //to repeat the same line verbatim.
    presence_penalty: 0.0, //Number between -2.0 and 2.0.
    //Positive values increase the model's likelihood
    //to talk about new topics.
    stop: ["#", ";"], //Up to 4 sequences where the API will stop
    //generating further tokens. The returned text
    //will not contain the stop sequence.
  };

  oHttp.send(JSON.stringify(data));

  if (txtOutput.value != "") txtOutput.value += "\n";
  txtOutput.value += "Me: " + sQuestion;
  txtMsg.value = "";
}

function TextToSpeech(s) {
  if (bTextToSpeechSupported == false) return;

  // Fetch the current state of the checkbox directly
  var chkMute = document.getElementById("chkMute");
  if (!chkMute || chkMute.checked) return; // Exit if muted

  oSpeechSynthesisUtterance = new SpeechSynthesisUtterance();

  if (oVoices) {
    var sVoice = selVoices.value;
    if (sVoice != "") {
      oSpeechSynthesisUtterance.voice = oVoices[parseInt(sVoice)];
    }
  }

  oSpeechSynthesisUtterance.onend = function () {
    //finished talking - can now listen
    if (oSpeechRecognizer && chkSpeak.checked) {
      oSpeechRecognizer.start();
    }
  };

  if (oSpeechRecognizer && chkSpeak.checked) {
    //do not listen to yourself when talking
    oSpeechRecognizer.stop();
  }

  oSpeechSynthesisUtterance.lang = selLang.value;
  oSpeechSynthesisUtterance.text = s;
  //Uncaught (in promise) Error: A listener indicated an
  //asynchronous response by returning true, but the message channel closed
  window.speechSynthesis.speak(oSpeechSynthesisUtterance);
}

function Mute() {
  document.getElementById("chkMute").addEventListener("change", function () {
    if (this.checked && window.speechSynthesis.speaking) {
      window.speechSynthesis.cancel(); // Immediately stop any ongoing speech
    }
  });
}

function SpeechToText() {
  if (oSpeechRecognizer) {
    if (chkSpeak.checked) {
      oSpeechRecognizer.start();
    } else {
      oSpeechRecognizer.stop();
    }

    return;
  }

  oSpeechRecognizer = new webkitSpeechRecognition();
  oSpeechRecognizer.continuous = true;
  oSpeechRecognizer.interimResults = true;
  oSpeechRecognizer.lang = selLang.value;
  oSpeechRecognizer.start();

  oSpeechRecognizer.onresult = function (event) {
    var interimTranscripts = "";
    for (var i = event.resultIndex; i < event.results.length; i++) {
      var transcript = event.results[i][0].transcript;

      if (event.results[i].isFinal) {
        txtMsg.value = transcript;
        Send();
      } else {
        transcript.replace("\n", "<br>");
        interimTranscripts += transcript;
      }
    }
  };

  oSpeechRecognizer.onerror = function (event) {};
}
function GetLastGPTMessage2() {
  var textarea = document.getElementById("txtOutput");
  var lastElement = textarea.value.split(/\n/);
  console.log(lastElement);
  var element = document.getElementById("sampleLastMessage");
  var lastthingy = lastElement.slice(-1);
  element.innerHTML = lastthingy;

  return lastElement[-1];
}
function GetLastGPTMessage() {
  var textarea = document.getElementById("txtOutput");
  var lastElement = textarea.value.split(/\n/);

  console.log(lastElement);

  var element = document.getElementById("sampleUserMessage");
  var lastthingy = lastElement.slice(-1);
  element.innerHTML = lastthingy;

  return renderUserMarkup(lastthingy);
  // return renderBotMarkup(lastthingy);
}

function getCurrentTime() {
  const now = new Date();
  let hours = now.getHours().toString().padStart(2, "0");
  let minutes = now.getMinutes().toString().padStart(2, "0");
  return `${hours}:${minutes}`;
}

function renderBotMarkup(message, isLoading = false) {
  const messages = document.querySelector(".messages");
  const currentTime = getCurrentTime();
  let markup;

  if (!isLoading) {
    let loadingMessage = document.getElementById("loadingMessage");
    if (loadingMessage) {
      loadingMessage.parentNode.removeChild(loadingMessage); // Remove the loading message from the DOM
    }
  }

  if (isLoading) {
    markup = `
      <div class="message new" id="loadingMessage">
          <figure class="avatar">
              <img src="img/avatar.jpg" />
          </figure>Loading...
          <div class="timestamp">${currentTime}</div>
      </div>`;
  } else {
    markup = `
      <div class="message new">
          <figure class="avatar">
              <img src="img/avatar.jpg" />
          </figure>${message}
          <div class="timestamp">${currentTime}</div>
      </div>`;
  }
  messages.insertAdjacentHTML("beforeend", markup);
}

function renderUserMarkup(message) {
  const messages = document.querySelector(".messages");
  const currentTime = getCurrentTime();
  const markup = `
    <div class="message message-personal new">
        ${message}
        <div class="timestamp">${currentTime}</div>
    </div>`;
  messages.insertAdjacentHTML("beforeend", markup);
}
