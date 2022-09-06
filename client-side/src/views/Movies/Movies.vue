<!--<template>
	<div class="movieList">
		<h1 class="subheading grey--text">Movies</h1>
		<div>
			<v-carousel v-model="model">
				<v-carousel-item v-for="(color, i) in colors" :key="color">
					<v-sheet :color="color" height="100%" tile>
						<v-row class="fill-height" align="center" justify="center">
							<div class="text-h2">Slide {{ i + 1 }}</div>
						</v-row>
					</v-sheet>
				</v-carousel-item>
			</v-carousel>
		</div>
		<v-container class="">
			<v-layout row wrap>
				<v-flex xs12 sm6 md2 lg3 v-for="movie in moviesList" :key="movie.id">
					<v-card class="text-center ma-5">
						<v-responsive class="pt-5">
							<v-avatar size="100" class="red lighten-2">
								<img src="@/assets/joker.jpg" alt="" />
							</v-avatar>
						</v-responsive>
						<v-card-text>
							<div class="subheading">{{ movie.title }}</div>
							<div class="grey--text">{{ movie.genre }}</div>
						</v-card-text>
						<v-card-actions>
							<v-btn outlined color="orange" class="p-50">
								<v-icon small left>message</v-icon>
								<router-link :to="{ name: 'Movie' }">
									<span>View</span>
								</router-link>
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
import getMovies from "@/utilities/getMovieList";

export default {
	data: () => ({
		drawer: null,
		model: 0,
		colors: ["primary", "secondary", "yellow darken-2", "red", "orange"],
	}),
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
</script>-->

<template>
	<div>
		<PopularMovies />
	</div>
</template>

<script>
import PopularMovies from "../../components/PopularMovies.vue";
export default {
	name: "Movies",
	components: {
		PopularMovies,
	},
};
</script>
