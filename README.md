# Dungeons and AI

Welcome to Dungeons and AI, an interactive chat-based adventure game where your choices shape the narrative. Set in a fantastical world, players engage in text-based dialogue with an AI-driven dungeon master, exploring mysterious lands, encountering unique characters, and battling fearsome creatures.

## Features

- **Dynamic Storytelling**: Engage with an AI dungeon master that adapts the story based on your choices.
- **Multiple Worlds**: Explore various worlds, each with its unique lore, challenges, and characters.
- **Character Customization**: Create your character by choosing from different races, classes, and backgrounds.
- **Interactive Chat**: Use the chat interface to interact with the world, make choices, and guide your adventure.
- **Voice Commands**: Enhanced gameplay experience with voice command features for an immersive storytelling experience.

## Getting Started

### Prerequisites

- [Git](https://git-scm.com/downloads)
- [Node.js](https://nodejs.org/en/download/)
- [npm](https://www.npmjs.com/get-npm)
- [OpenAI API Key](https://platform.openai.com/api-keys)
- Visual Studio 2022
- SQL Server Express or SQL Server Developer Edition
- Visual Studio Code or any other code editor

To get started with Dungeons and AI, follow these steps:

1. **Clone the Repository**

   ```sh
   git clone https://github.com/Ibryam512/Dungeons-and-AI.git
   ```

2. To run the frontend, navigate to the `DungeonsAndAIFrontEnd` directory, and install live-server using the following command:

   ```sh
   npm install -g live-server
   ```

3. **Install Dependencies**

   ```sh
   npm install
   ```

4. **Insert Your API Key**

- Go to the [OpenAI API](https://platform.openai.com/api-keys) and sign up for an API key.
- In `DungeonsAndAIFrontEnd/js/script.js` replace `OPENAI_API_KEY` with your API key.
- **Keep in mind that if you are using a model that's different** from GPT 3.5 Turbo, you will need to replace this line in the `script.js` file:
  ```javascript
  var sModel = "gpt-3.5-turbo";
  ```
  with your model name.

5. **Run the Application**

   ```sh
   npm start
   ```

   Access the application through `http://localhost:8080` in your web browser.

6. **To run the backend,** open the solution file `DungeonsAndArtificalIntelligenceAPI.sln` located in the `DungeonsAndArtificalIntelligenceAPI` folder in Visual Studio 2022 and run the application.
7. Update the `appsettings.json` file in the `DungeonsAndArtificalIntelligenceAPI` project with your SQL Server connection string if you are not using SQL Server Express.
8. **To run the database migrations,** open the Package Manager Console in Visual Studio 2022 and run the following command:
   ```sh
   Update-Database
   ```

## How to Play

- Start by selecting a game category to determine the theme of your adventure.
- Customize your character by choosing a world, race, and class.
- Begin your journey by asking questions or making statements like "Where am I?" or "Let's begin."
- Navigate the story through your choices and interactions with the AI.

## Contributing

Contributions to Dungeons and AI are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## License

Dungeons and AI is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Acknowledgments

- OpenAI for the GPT API used to drive the game's AI.
- [Contributors and supporters](https://github.com/Ibryam512/Dungeons-and-AI/graphs/contributors) who have helped shape the project.
