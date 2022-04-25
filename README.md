# expandit-challenge
The repository containing the full application fot the challenge proposed by expandit

# Develop



# Run

The only requirement to run the project out of the box, is [docker compose](https://docs.docker.com/compose/install/), each container will take care of its own dependencies.
You also will need to have ports 4000 and 5000 available since those are used to serve frontend and backend respectively.

## How to

- Place youreself in the root of the project, the folder where file `docker-compose.yml` is, and run `docker-compose up --build`

    - If it's not the first time you run the project, you can use only `docker-compose up` since the images are already created

- After the build proccess is finished, three containers will be up

    - mariadb: a simple server with a my sql database, used to store all data

    - backend: a not net core 6 backend api, it conmmunicates with the database, and provides endpoints to manage contacts and groups

    - frontend: an angular 13 frontend application that consumes the api and display in an nice interface

- Access the interface in `http://localhost:4000/`

- The backend is available at `http://localhost:5000/`, so you can test it with postman or something alike

## Known issues

- After building, the 3 containers will each try to up at the same time, and sometimes the backend is up before the database, this throws an error when trying to run migrations, since the database is not available yet. To continue just wait for mariadb container to start accepting connections and up the backend container again, to do this you can use the control panel in windows and start the container again, or you can use the console in both windows or linux and run `docker-compose start backend`.
This is solvable, i can use a script as an entrypoint to wait for the db to allow connections, but the setup is a little time consuming for the scope of this challenge

- When using the interface, you have to enter using `http://localhost:4000`, if you enter by some other valid url like `http://localhost:4000/contracts`, the nginx of the container will return a 404 error. So please enter by the main url and dont refresh the page. This is solvable using a custom nginx configuration file, but (again) is too much time consuming for the scope of this challenge

