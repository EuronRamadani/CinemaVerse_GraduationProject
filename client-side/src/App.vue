<template>
	<!-- App.vue -->

	<v-app>
		<!-- <v-navigation-drawer app>
  </v-navigation-drawer> -->

		<!-- Sizes your content based upon application components -->
		<!-- <div>
			<router-view></router-view>
			<dashboard :isLoggedIn="isLoggedIn" :auth="auth" />
		</div> -->
		<dashboard />
		<v-content class="ma-4">
			<router-view></router-view>
		</v-content>
		<!-- <v-main else> -->
		<!-- <navigation :isLoggedIn="isLoggedIn" :auth="auth" /> -->
		<!-- Provides the application the proper gutter -->
		<!-- <v-container fluid> -->
		<!-- If using vue-router -->
		<!-- </v-container> -->
		<!-- <app-footer /> -->
		<!-- </v-main> -->
	</v-app>
</template>

<script>
// import Navigation from "@/components/navbar/Navigation.vue";
// import AppFooter from "@/components/footer/Footer.vue";
import { getAuth, onAuthStateChanged } from "firebase/auth";
import dashboard from "./views/Admin/Dashboard.vue";
import "firebase/auth";
// \Navigation, AppFooter,
export default {
	components: { dashboard },
	name: "App",

	data() {
		// return {
		// 	isAdmin: true,
		// };
	},
	computed: {
		// dashboard() {
		// 	return this.$router.name === "Admin";
		// },
	},
	mounted() {
		this.auth = getAuth();
		onAuthStateChanged(this.auth, (user) => {
			if (user) {
				this.isLoggedIn = true;
			} else {
				this.isLoggedIn = false;
			}
		});
	},
};
</script>
