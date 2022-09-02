<template>
	<div>
		<input type="text" v-model="search" placeholder="search cinemas" />
		<div class="card" v-for="cinema in filter" :key="cinema.id">
			<v-card :loading="loading" class="mx-auto my-12" max-width="374">
				<template slot="progress">
					<v-progress-linear
						color="deep-purple"
						height="10"
						indeterminate
					></v-progress-linear>
				</template>

				<v-img
					height="250"
					src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
				></v-img>

				<v-card-title>{{ cinema.title }}</v-card-title>

				<v-divider class="mx-4"></v-divider>

				<v-card-title>Tonight's availability</v-card-title>

				<v-card-actions>
					<v-btn color="deep-purple lighten-2" text :to="{ name: 'Cineplexx' }">
						View Films
					</v-btn>
				</v-card-actions>
			</v-card>
		</div>
	</div>
</template>

<script>
export default {
	name: "Cinemas",
	data() {
		return {
			cinemas: [
				{ title: "Roni", vendi: "Prishtine" },
				{ title: "test", vendi: "Ferizaj" },
			],
			// cinemas: [],
			search: "",
		};
	},
	computed: {
		filter() {
			return this.cinemas.filter((temp) => {
				return temp.title.match(this.search);
			});
		},
	},
};
</script>

<style scoped>
.card {
	flex-direction: row;
	justify-content: center;
}
input {
	border: 1px solid black;
	position: relative;
	left: 85%;
}
</style>
