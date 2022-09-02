export default [
	{
		path: "/films",
		name: "Films",
		component: () =>
			import(/* webpackChunkName: "register" */ "../../views/Films/Films.vue"),
	},
	{
		path: "/cineplexx/films",
		name: "Cineplexx",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Films/Cineplexx/CineplexxFilms.vue"
			),
	},
	{
		path: "/abc/films",
		name: "ABC",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Films/ABC/ABCfilms.vue"
			),
	},
];
