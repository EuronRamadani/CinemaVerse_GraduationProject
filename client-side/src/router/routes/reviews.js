export default [
	{
		path: "/movies/:movieId/:eventId",
		name: "AddReview",
		component: () =>
			import(
				/* webpackChunkName: "movie-public-details" */ "../../views/Movie/AddReview.vue"
			),
	}
];
