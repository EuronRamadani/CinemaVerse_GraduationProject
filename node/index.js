"use strict";

const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
const config = require("./config");
const userRoutes = require("./Routes/user-routes");
const mongoose = require("mongoose");
const ReviewRoute = require("./Routes/ReviewRoute");

const connectionSting = "mongodb://localhost:27017/Kinema";

mongoose.connect(connectionSting).then(() => {
	console.log("Connected to mongodb on: " + connectionSting);
	const app = express();

	app.use(express.json());
	app.use(cors());
	app.use(bodyParser.json());

	app.use("/api", userRoutes.routes);
	app.use("/review", ReviewRoute.routes);

	app.listen(config.port, () =>
		console.log("App is listening on url http://localhost:" + config.port)
	);
});
