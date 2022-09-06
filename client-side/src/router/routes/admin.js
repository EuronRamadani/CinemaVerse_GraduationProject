export default [
	{
		path: "/admin",
		name: "Admin",
		meta: {
			requiresAuth: true,
		},
		component: () =>
			import(/* webpackChunkName: "admin" */ "../../views/Admin/Dashboard.vue"),
	},
	{
		path: "/admin/cinemas",
		name: "CinemasDashboard",
		meta: {
			requiresAuth: true,
		},
		component: () =>
			import(
				/* webpackChunkName: "admin" */ "../../views/Admin/Cinemas/CinemasDashboard.vue"
			),
	},
	{
		path: "/admin/films",
		name: "FilmsDashboard",
		meta: {
			requiresAuth: true,
		},
		component: () =>
			import(
				/* webpackChunkName: "admin" */ "../../views/Admin/Films/FilmsDashboard.vue"
			),
	},
	{
		path: "/admin/events",
		name: "EventsDashboard",
		meta: {
			requiresAuth: true,
		},
		component: () =>
			import(
				/* webpackChunkName: "admin" */ "../../views/Admin/Events/EventsDashboard.vue"
			),
	},
];
