export default [
	{
		path: "/cinemas",
		name: "Cinemas",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Cinemas/Cinemas.vue"
			),
	},
];
