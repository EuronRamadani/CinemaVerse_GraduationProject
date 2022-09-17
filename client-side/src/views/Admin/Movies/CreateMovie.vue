<template>
	<div>
		<h2>Create Movie</h2>
		<hr />
		<div class="container mt-5">
			<validation-observer ref="observer" v-slot="{ invalid }">
				<form @submit.prevent="submit">
					<validation-provider
						v-slot="{ errors }"
						name="Title"
						rules="required|min:3"
					>
						<v-text-field
							v-model="title"
							:error-messages="errors"
							label="Title"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Description"
						rules="required"
					>
						<v-textarea
							v-model="description"
							:error-messages="errors"
							label="Description"
							outlined
							required
						></v-textarea>
					</validation-provider>
					<validation-provider v-slot="{ errors }" name="City" rules="required">
						<v-text-field
							v-model="director"
							:error-messages="errors"
							label="Director"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Trailer Link"
						rules="required"
					>
						<v-text-field
							v-model="trailerLink"
							:error-messages="errors"
							label="Trailer Link"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Release Year"
						rules="required"
					>
						<v-text-field
							v-model.number="releaseYear"
							type="number"
							:error-messages="errors"
							label="Release Year"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Release Date"
						rules="required"
					>
						<v-text-field
							v-model="releaseDate"
							type="date"
							:error-messages="errors"
							label="Release Date"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Country"
						rules="required"
					>
						<v-text-field
							v-model="country"
							:error-messages="errors"
							label="Country"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Language"
						rules="required"
					>
						<v-text-field
							v-model="language"
							:error-messages="errors"
							label="Language"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Genre"
						rules="required"
					>
						<v-text-field
							v-model="genre"
							:error-messages="errors"
							label="Genre"
							outlined
							required
						></v-text-field>
					</validation-provider>
					<validation-provider
						v-slot="{ errors }"
						name="Length"
						rules="required"
					>
						<v-text-field
							v-model.number="length"
							type="number"
							:error-messages="errors"
							label="Length"
							outlined
							required
						></v-text-field>
					</validation-provider>

					<v-btn
						color="success"
						type="submit"
						:disabled="invalid"
						class="mr-2"
						@click="createMovie()"
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
			title: "",
			description: "",
			director: "",
			trailerLink: "",
			releaseYear: 0,
			releaseDate: "",
			country: "",
			language: "",
			genre: "",
			length: 0,
		};
	},
	methods: {
		submit() {
			this.$refs.observer.validate();
		},
		createMovie() {
			const movie = {
				title: this.title,
				description: this.description,
				director: this.director,
				trailerLink: this.trailerLink,
				releaseYear: this.releaseYear,
				releaseDate: this.releaseDate,
				country: this.country,
				language: this.language,
				genre: this.genre,
				length: this.length,
			};
			this.$store
				.dispatch("createMovie", movie)
				.then(() => {
					this.clear();
					this.$router.push({
						name: "MoviesDashboard",
					});
				})
				.catch((error) => {
					this.errorToast(
						error.response?.data?.errors[0] ||
							"Something went wrong while creating movies!"
					);
				});
		},
		clear() {
			this.title = "";
			this.description = "";
			this.director = "";
			this.trailerLink = "";
			this.releaseYear = 0;
			this.releaseDate = "";
			this.country = "";
			this.language = "";
			this.genre = "";
			this.length = 0;
			this.$refs.observer.reset();
		},
	},
};
</script>
