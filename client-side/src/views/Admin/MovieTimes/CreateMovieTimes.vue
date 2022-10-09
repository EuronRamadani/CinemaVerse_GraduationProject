<template>
	<div>
		<v-row>
			<v-col cols="9">
				<h2>Movies</h2>
			</v-col>
			<v-col cols="3">
				<v-select
					v-model="selectedCinema"
					:items="getObjectOptionsName(cinemas)"
					@change="changeCinema()"
					solo
					label="Select Cinema"
				></v-select>
			</v-col>
		</v-row>
		<hr />
		<div class="container mt-5">
			<validation-observer ref="observer" v-slot="{ invalid }">
				<form @submit.prevent="submit">
					<v-col class="d-flex" cols="12" sm="6">
						<v-select label="Hall Id" dense>
							<option v-for="item in halls" :key="item.id">
								{{ item.hallNumber }}
							</option></v-select
						>
					</v-col>
					<!-- <validation-provider
            v-slot="{ errors }"
            name="Hall Id"
            rules="required"
          >
            <v-text-field
              v-model="hallId"
              type="number"
              :error-messages="errors"
              label="Hall Id"
              outlined
              required
            ></v-text-field>
          </validation-provider> -->
					<validation-provider
						v-slot="{ errors }"
						name="Start Time"
						rules="required"
					>
						<v-text-field
							v-model="startTime"
							type="datetime-local"
							:error-messages="errors"
							label="Start Time"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="End Time"
						rules="required"
					>
						<v-text-field
							v-model="endTime"
							type="datetime-local"
							:error-messages="errors"
							label="End Time"
							outlined
							required
						></v-text-field>
					</validation-provider>

					<v-btn
						color="success"
						type="submit"
						:disabled="invalid"
						class="mr-2"
						@click="createMovieTime()"
					>
						Submit
					</v-btn>
				</form>
			</validation-observer>
		</div>
	</div>
</template>

<script>
import { required, numberInt, minValueRule } from "@/helpers/validations";
import {
	ValidationObserver,
	ValidationProvider,
	setInteractionMode,
} from "vee-validate";

setInteractionMode("eager");

export default {
	components: {
		ValidationProvider,
		ValidationObserver,
	},
	data() {
		return {
			required,
			numberInt,
			minValueRule,
			cinemaId: null,
			hallId: 0,
			startTime: null,
			endTime: null,
			selectedCinema: null,
			items: ["Foo", "Bar", "Fizz", "Buzz"],
		};
	},
	computed: {
		cinemas() {
			return this.$store.state.cinemas.cinemas;
		},
		halls() {
			return this.$store.state.halls.halls;
		},
	},
	created() {
		this.cinemaId = this.$route.params.cinemaId;
		this.movieId = this.$route.params.movieId;
		console.log("halls", this.halls);
	},
	methods: {
		onRefresh() {
			this.getHalls(this.selectedCinema);
		},
		getCinema() {
			this.$store.dispatch("getCinema").catch((error) => {
				this.errorToast(
					error.response?.data?.errors[0] ||
						"Something went wrong while fetching cinemas!"
				);
			});
		},
		submit() {
			this.$refs.observer.validate();
		},
		getCinemas() {
			this.$store
				.dispatch("getCinemas")
				.then(() => {
					if (this.selectedCinema == null) {
						this.selectedCinema = this.cinemas[0];
					}
					this.getMovies(this.selectedCinema);
				})
				.catch((error) => {
					this.errorToast(
						error.response?.data?.errors[0] ||
							"Something went wrong while fetching cinemas!"
					);
				});
		},
		getHalls(selectedCinema) {
			this.$store.dispatch("getHalls", selectedCinema.id).catch((error) => {
				this.errorToast(
					error.response?.data?.errors[0] ||
						"Something went wrong while fetching Halls!"
				);
			});
		},
		changeCinema() {
			if (this.selectedCinema != null) {
				this.getHalls(this.selectedCinema);
			}
		},
		createMovieTime() {
			const movieTime = {
				hallId: this.hallId,
				startTime: this.startTime,
				endTime: this.endTime,
			};
			this.$store
				.dispatch("createMovieTime", {
					cinemaId: this.cinemaId,
					movieId: this.movieId,
					movieTime: movieTime,
				})
				.then(() => {
					this.clear();
					this.$router.push({
						name: "MoviesDashboard",
					});
				})
				.catch((error) => {
					console.log(error);
					this.errorToast(
						error.response?.data ||
							"Something went wrong while creating movie times!"
					);
				});
		},
		clear() {
			this.hallId = 0;
			this.startTime = null;
			this.endTime = null;
			this.$refs.observer.reset();
		},
	},
};
</script>
