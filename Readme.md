<h1>StockTalk</h1>

- Installation:

1. First of all, you need to have the dotnet sdk installed and docker.
2. Open the terminal or bash in the root of folder project and run the command: `dotnet tool restore`, this command will install tools necessary;
3. After this run on terminal: `.\build.ps1 --Target=Infra`, will create containers with technologies necessary for infrastructure (database and rabbitmq), whether you are using the OS linux or MacOs run `build.sh --Target=Infra`;
4. Now run the command: `.\build.ps1 --Target=App`( `build.sh --Target=App`, linux and MacOs), it will create the containers with the webapi and worker.

The frontend application, has two options: Clone the project and on the root folder run: `npm i` and `ng s`(this option is more speedy.), a another it's, in the root path have the cake file run the command: `dotnet tool restore`, after `.\build.ps1 --Target=WebApp`(build.sh --Target=Webapp);

- Usage:
1. The first page it's the login page, if to first time in the application, will be necessary signup, have a button to this.;
2. After login, the home page it's where you can create the chatsrooms and list the existed; Have a button where you can create it;
3. In the chat room, to call the bot press the `/` and will appear a window, enter the symbol of the stock you want to search and in the chat will appear a message with result.
