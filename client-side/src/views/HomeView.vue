<template>
	<div>
		<!-- <h1>Homepage</h1> -->
		<UpcomingMovies />
		<div>
			<v-main class="grey lighten-2">
				<v-container>
					<v-row>
						<template v-for="n in 1">
							<v-col :key="n" class="mt-1" cols="12">
								<strong>Qitu mi shfaq do movies</strong>
							</v-col>

							<v-col v-for="j in 6" :key="`${n}${j}`" cols="6" md="2">
								<v-sheet height="150">
									<!-- <img :src="cinemas" alt="" /> -->
								</v-sheet>
							</v-col>
						</template>
					</v-row>
				</v-container>
			</v-main>
		</div>

		<div>
			<v-main class="grey lighten-2">
				<v-container>
					<v-row>
						<p>evente</p>
						<v-col v-for="n in 6" :key="n" cols="6">
							<v-card height="200"></v-card>
						</v-col>
					</v-row>
				</v-container>
			</v-main>
		</div>
		<div class="container">
			<section class="about-container container">
				<div class="about-sections">
					<img class="offer-image" :src="photo1" alt="" />
					<div class="description">
						<h3>About Us</h3>
						<p>
							CinemaVerse, as the first project in our region for the union of
							cinemas, is a big project that is doing very well because it is
							easy to use and you have everything in one place. CinemaVerse was
							created with the aim of bringing customers together in one
							platform, through comments, movie selection and online payment.
						</p>
					</div>
				</div>
				<div class="about-sections">
					<div class="description">
						<h3>Our Mission</h3>
						<p>
							As an application that is at the beginning, we are trying to be
							the best in the work we do, where we still continue to work
							towards the quality and maintenance of the application. Our
							mission for the future is to spread even more through the
							neighboring countries, to have as many cinemas from different
							countries as possible and in this way to expand even more.
						</p>
					</div>
					<img class="offer-image" :src="photo2" alt="" />
				</div>
				<div class="about-sections">
					<img class="offer-image" :src="photo3" alt="" />
					<div class="description">
						<h3>Experience</h3>
						<p>
							Considering that CinemaVerse is the first application in our
							market, it has been well received by our clients, where we have
							also received feedback, very good comments. So we are working for
							the best
						</p>
					</div>
				</div>
			</section>
		</div>
	</div>
</template>

<script>
import UpcomingMovies from "../components/SlideshowMovies.vue";
// import { mapActions, mapGetters } from "vuex";

export default {
	name: "Home",
	components: {
		UpcomingMovies,
	},
	data() {
		return {
			drawer: null,
			photo1:
				"https://www.cineplexx-ks.eu/GenticsImageStore/952/353/force//media/mobile/ks/center/5.jpg",
			photo2:
				"https://qkk-rks.com/uploads/images/2018/March/12/auto_cineplexx1520872988.jpg",
			photo3:
				"https://qkk-rks.com/uploads/images/2018/March/12/auto_kinoabc11520873925.jpg",
		};
	},
	created() {
		this.getCinemas();
		console.log("Created", this.cinemas);
	},
	computed: {
		cinemas() {
			return this.$store.state.cinemas.cinemas;
			// [0].photos[0].imgClientPath
		},
	},
	methods: {
		getCinemas() {
			this.$store.dispatch("getCinemas").catch((error) => {
				this.errorToast(
					error.response?.data?.errors[0] ||
						"Something went wrong while fetching cinemas!"
				);
			});
		},
	},
};
</script>

<style scoped>
.container {
	width: 95%;
	height: 70%;
	margin: auto;
	overflow: hidden;
}
.offer-image {
	height: 300px;
}
.description {
	padding: 50px;
}
.about-container {
	display: flex;
	flex-direction: column;
}
.about-sections {
	display: flex;
	align-items: center;
	background-color: #fff;
	margin: 50px 0 50px 0;
}
</style>
