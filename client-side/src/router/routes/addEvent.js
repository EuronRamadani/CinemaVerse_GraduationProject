export default [
	{
		path: "/addEvent",
		name: "AddEvent",
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Events/eventsComponents/AddEvent.vue"
			),
	},
];
