<template>
	<div class="projects">
		<h1 class="subheading grey--text">Movies</h1>
		<v-container>
			<v-layout row wrap class="mb-4">
				<v-tooltip bottom>
					<template v-slot:activator="{ on }">
						<v-btn
							small
							outlined
							color="green"
							@click="sortBy('title')"
							class="mr-2"
							dark
							v-on="on"
						>
							<v-icon left small>folder</v-icon>
							<span class="caption text-lowercase">Sort by title</span>
						</v-btn>
					</template>
					<span>Sort movies</span>
				</v-tooltip>
				<!-- <v-tooltip bottom>
					<template v-slot:activator="{ on }">
						<v-btn
							small
							outlined
							color="blue"
							@click="sortBy('title')"
							class="mr-2"
							dark
							v-on="on"
						>
							<v-icon left small>person1</v-icon>
							<span class="caption text-lowercase">By name</span>
						</v-btn>
					</template>
					<span>Sort person1</span>
				</v-tooltip> -->
			</v-layout>
			<v-card flat v-for="movie in moviesList" :key="movie.id" class="mb-1">
				<v-layout row wrap :class="`pa-3 project ${movie.title}`">
					<v-flex xs12 md2>
						<div class="caption grey--text">Title</div>
						<div>{{ movie.title }}</div>
					</v-flex>
					<v-flex xs6 sm4 md2>
						<div class="caption grey--text">Trailer Link</div>
						<div>{{ movie.trailerLink }}</div>
					</v-flex>
					<v-flex xs6 sm4 md2>
						<div class="caption grey--text">Release Year</div>
						<div>{{ movie.releaseYear }}</div>
					</v-flex>
					<v-flex xs6 sm4 md2>
						<div class="caption grey--text">Country</div>
						<div>{{ movie.country }}</div>
					</v-flex>
					<v-flex xs6 sm4 md2>
						<div class="caption grey--text">Language</div>
						<div>{{ movie.language }}</div>
					</v-flex>
					<v-flex xs6 sm4 md1>
						<div class="caption grey--text">Genre</div>
						<div>{{ movie.genre }}</div>
					</v-flex>
					<v-flex xs6 sm4 md1>
						<button type="submit">Delete</button>
						<button type="submit">Edit</button>
					</v-flex>
				</v-layout>
			</v-card>
		</v-container>
	</div>
</template>

<script>
// @ is an alias to /src
import { mapGetters } from "vuex";
import getMovies from "@/utilities/getFilm";
export default {
	name: "MoviesDashboard",
	components: {},
	data: () => ({}),
	methods: {
		sortBy(prop) {
			this.moviesList.sort((a, b) => (a[prop] < b[prop] ? -1 : 1));
		},
		async fetchMovies() {
			const result = await getMovies();
			this.$store.dispatch("fetchMovies", result.result);
		},
	},
	computed: {
		...mapGetters({
			moviesList: "moviesList",
		}),
	},
	created() {
		this.fetchMovies();
	},
};
</script>
<style scoped></style>
