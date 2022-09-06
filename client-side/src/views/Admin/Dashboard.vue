<!-- <template>
	<body class="admin">
		<div class="container-fluid p-0">
			<div class="admin-tabs ml-2">
				<v-tabs>
					<v-tab @click="changeTab('Cinema')" class="p-2">Cinemas</v-tab>
					<v-tab @click="changeTab('Film')" class="p-2">Films</v-tab>
					<v-tab @click="changeTab('Event')" class="p-2">Events</v-tab>
				</v-tabs>
			</div>
			<div class="container admin-tab-body">
				<div v-if="currentTab === 'Cinema'">
					<cinema-dashboard />
				</div>
				<div v-if="currentTab === 'Film'">
					<film-dashboard />
				</div>
				<div v-if="currentTab === 'Event'">
					<event-dashboard />
				</div>
			</div>
		</div>
	</body>
</template>

<script>
import CinemaDashboard from "../Admin/Cinemas/CinemasDashboard.vue";
import FilmDashboard from "../Admin/Films/FilmsDashboard.vue";
import EventDashboard from "../Admin/Events/EventsDashboard.vue";

export default {
	name: "Dashboard",
	components: {
		CinemaDashboard,
		FilmDashboard,
		EventDashboard,
	},
	data() {
		return {
			adminEmail: "",
			functionMsg: null,
			currentTab: "Dashboard",
		};
	},
	methods: {
		changeTab(tab) {
			this.currentTab = tab;
		},
	},
};
</script>

<style></style>-->

<template>
	<nav>
		<v-app-bar color="red" dark app>
			<v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
			<v-toolbar-title class="text-uppercase">
				<span class="font-weight-light">AAE</span>
				<span>IdeaPro</span>
			</v-toolbar-title>
			<v-spacer></v-spacer>
			<v-menu offset-y>
				<template v-slot:activator="{ on }">
					<v-btn text v-on="on">
						<v-icon left>expand_more</v-icon>
						<span>Menu</span>
					</v-btn>
				</template>
				<v-list flat>
					<v-list-item
						v-for="link in links"
						:key="link.text"
						router
						:to="link.route"
						active-class="border"
					>
						<v-list-item-title>{{ link.text }}</v-list-item-title>
					</v-list-item>
				</v-list>
			</v-menu>
			<v-btn text>
				<span>Exit</span>
				<v-icon right>exit_to_app</v-icon>
			</v-btn>
		</v-app-bar>
		<v-navigation-drawer v-model="drawer" dark app class="red darken-4">
			<v-layout column align-center>
				<v-flex class="mt-5">
					<v-avatar size="100">
						<img src="/img1.png" alt="" />
					</v-avatar>
					<p class="white--text subheading mt-1 text-center">Username</p>
				</v-flex>
				<v-flex class="mt-4 mb-4"> </v-flex>
			</v-layout>
			<v-list flat>
				<v-list-item
					v-for="link in links"
					:key="link.text"
					router
					:to="link.route"
					active-class="border"
				>
					<v-list-item-action>
						<v-icon>{{ link.icon }}</v-icon>
					</v-list-item-action>
					<v-list-item-content>
						<v-list-item-title>{{ link.text }}</v-list-item-title>
					</v-list-item-content>
				</v-list-item>
			</v-list>
		</v-navigation-drawer>
	</nav>
</template>
<script>
// import Popup from "./Popup.vue";
export default {
	data: () => ({
		drawer: true,
		links: [
			{ icon: "dashboard", text: "Cinema", route: "/admin/cinemas" },
			{ icon: "folder", text: "Movies", route: "/admin/films" },
			{ icon: "person", text: "Events", route: "/admin/events" },
		],
	}),
	// components: {
	// 	Popup,
	// },
};
</script>
<style scoped>
.border {
	border-left: 4px solid #0ba518;
}
</style>
