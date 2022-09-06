export default [
	{
		path: "/createEvent",
		name: "Create Event",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Events/crudEvents/CreateEvent.vue"
			),
	},
];
