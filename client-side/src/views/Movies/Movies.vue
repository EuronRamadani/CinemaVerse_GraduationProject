<template>
	<div class="movieList">
		<h1 class="subheading grey--text">Movies</h1>
		<v-container>
			<v-layout row wrap>
				<v-flex xs12 sm6 md4 lg3 v-for="movie in moviesList" :key="movie.id">
					<v-card class="text-center ma-3">
						<v-responsive class="pt-4">
							<v-avatar size="100" class="red lighten-2">
								<img :src="movie" alt="" />
							</v-avatar>
						</v-responsive>
						<v-card-text>
							<div class="subheading">{{ movie.title }}</div>
							<div class="grey--text">{{ movie.genre }}</div>
						</v-card-text>
						<v-card-actions>
							<v-btn outlined color="orange">
								<v-icon small left>message</v-icon>
								<span>View</span>
							</v-btn>
						</v-card-actions>
					</v-card>
				</v-flex>
			</v-layout>
		</v-container>
	</div>
</template>

<script>
import { mapGetters } from "vuex";
import getMovies from "@/utilities/getFilm";
export default {
	data: () => ({ drawer: null }),
	computed: {
		...mapGetters({
			moviesList: "moviesList",
		}),
	},
	created() {
		this.fetchMovies();
	},
	methods: {
		async fetchMovies() {
			const result = await getMovies();
			this.$store.dispatch("fetchMovies", result.result);
		},
	},
};
</script>
