# expandit-challenge
The repository containing the full application for the challenge proposed by expandit

# Develop

In the root of the project there us a docker-compose file and a projects folder. Inside the projects folder there us the two folders that correspond to the backend and frontend services. Ideally, each one of these would be a different git project, but considering the scope of this challenge, it's more practical if they are both in the same repository.

## How to

**Backend:** If you need to develop the backend use the folder `projects/Challenge`, i would recomend to open it individually, since its easier to run it, I used vs code to develop and run the project but you can use whatever you want. In vs code press f5 and it will recomend configurations to run as a dotnet application. You need to have [dot net core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed in order to develop the project.  
A postman collection is provided in file `Challenge.postman_collection.json` to make it easier to test the application.

**Frontend:** If you need to develop the frontend, use the folder `projects/Frontend`, you dont need to open this folder individually, but you need to place yourself in that folder in a terminal of your choice. I used vs code imbued terminal but you can use whatever you want as long as it can access node and angular cli's. To develop the frontend you will need both [node](https://nodejs.org/en/download/) and [angular](https://angular.io/guide/setup-local). Once in the correct folder, run `ng s --port 4000`, the port is optional and it defaults to 4200, i just used the same port configured in the docker-compose file.



# Run

The only requirement to run the project out of the box, is [docker compose](https://docs.docker.com/compose/install/), each container will take care of its own dependencies.
You also will need to have ports 4000 and 5000 available since those are used to serve frontend and backend respectively.

## How to

- Place youreself in the root of the project, the folder where file `docker-compose.yml` is, and run `docker-compose up --build`

    - If it's not the first time you run the project, you can use just `docker-compose up` since the images are already created

- After the build proccess is finished, three containers will be up

    - mariadb: a simple server with a my sql database, used to store all data

    - backend: a not net core 6 backend api, it conmmunicates with the database, and provides endpoints to manage contacts and groups

    - frontend: an angular 13 frontend application that consumes the api and display in an nice interface

- Access the interface in `http://localhost:4000/`

- The backend is available at `http://localhost:5000/`, so you can test it with postman or something alike

## Known issues

- After building, the 3 containers will each try to up at the same time, and sometimes the backend is up before the database, this throws an error when trying to run migrations, since the database is not available yet. To continue just wait for mariadb container to start accepting connections and up the backend container again, to do this you can use the control panel in windows and start the container again, or you can use the console in both windows or linux and run `docker-compose start backend`.
This is solvable, i can use a script as an entrypoint to wait for the db to allow connections, but the setup is a little time consuming for the scope of this challenge EDIT: this is currently solved

- When using the interface, you have to enter using `http://localhost:4000`, if you enter by some other valid url like `http://localhost:4000/contracts`, the nginx of the container will return a 404 error. So please enter by the main url and dont refresh the page. This is solvable using a custom nginx configuration file, but (again) is too much time consuming for the scope of this challenge

# Future work

- The file upload is completely missing
- The frontend can use a better design, this was made this way to show off all functionalities of the challenge and not really taking user experience into consideration
- There are tests missing due to time constrains and lack my experience in implementing them
- There are validations missing, for example, it is possible to submit any string as a phone number
