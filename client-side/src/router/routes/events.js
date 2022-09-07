export default [
	{
		path: "/events",
		name: "Events",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Events/Events.vue"
			),
	},
];
