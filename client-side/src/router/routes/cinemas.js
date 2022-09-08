export default [
	{
		path: "/cinemas",
		name: "Cinemas",
		meta: {
			layout: "public",
		},
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../views/Cinemas/Cinemas.vue"
			),
	},
	{
		path: "/cinemas/CineplexxPrice",
		name: "CineplexxPrice",
		meta: {
			layout: "public",
		},
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../components/CineplexxPrice.vue"
			),
	},
	{
		path: "/cinemas/CineplexxInfo",
		name: "CineplexxInfo",
		meta: {
			layout: "public",
		},
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../components/CineplexxInfo.vue"
			),
	},
	{
		path: "/cinemas/AbcPrice",
		name: "AbcPrice",
		meta: {
			layout: "public",
		},
		component: () =>
			import(
				/* webpackChunkName: "register" */ "../../components/AbcPrice.vue"
			),
	},
	{
		path: "/cinemas/AbcInfo",
		name: "AbcInfo",
		meta: {
			layout: "public",
		},
		component: () =>
			import(/* webpackChunkName: "register" */ "../../components/AbcInfo.vue"),
	},
];
