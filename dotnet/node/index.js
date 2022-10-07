"use strict";

const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
const config = require("./config");
const mongoose = require("mongoose");

//ROUTES
const userRoutes = require("./Routes/user-routes");
// const ReviewRoute = require("./Routes/ReviewRoute");

const connectionSting = "mongodb://localhost:27017/cinemaverse_db";

mongoose.connect(connectionSting).then(() => {
  console.log("Connected to mongodb on: " + connectionSting);
  const port = config.port || 1000;

  const app = express();

  app.use(express.json());
  app.use(cors());
  app.use(bodyParser.json());

  app.use("/api", userRoutes.routes);
  //   app.use("/review", ReviewRoute.routes);

  app.listen(port, () =>
    console.log("App is listening on url http://localhost:" + config.port)
  );
});
