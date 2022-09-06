export default [
	{
		path: "/event",
		name: "Event",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Events/Event.vue"
			),
	},
];
