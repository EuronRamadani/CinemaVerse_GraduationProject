<template>
	<body class="admin">
		<div class="container-fluid p-0">
			<div class="admin-tabs ml-2">
				<v-tabs>
					<!-- <v-tab @click="changeTab('Dashboard')" class="p-2">Dashboard</v-tab> -->
					<v-tab @click="changeTab('Cinema')" class="p-2">Cinemas</v-tab>
					<v-tab @click="changeTab('Film')" class="p-2">Films</v-tab>
					<v-tab @click="changeTab('Event')" class="p-2">Events</v-tab>
				</v-tabs>
			</div>
			<div class="container admin-tab-body">
				<!-- <div v-if="currentTab === 'Dashboard'">
					<admin-dashboard />
				</div> -->
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

<style></style>
