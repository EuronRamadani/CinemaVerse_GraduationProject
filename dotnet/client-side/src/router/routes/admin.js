export default [
	{
		path: "/admin",
		name: "Admin",
		meta: {
			requiresAuth: true,
			layout: "dashboard",
		},
		component: () =>
			import(/* webpackChunkName: "admin" */ "../../views/Admin/Dashboard.vue"),
	},
	{
		path: "/admin/events",
		name: "EventsDashboard",
		meta: {
			requiresAuth: true,
			layout: "dashboard",
		},
		component: () =>
			import(
				/* webpackChunkName: "admin" */ "../../views/Admin/Events/EventsDashboard.vue"
			),
	},
	{
		path: "/admin/actors",
		name: "ActorsDashboard",
		meta: {
			requiresAuth: true,
			layout: "dashboard",
		},
		component: () =>
			import(
				/* webpackChunkName: "admin" */ "../../views/Admin/Actors/ActorsDashboard"
			),
	},
];
