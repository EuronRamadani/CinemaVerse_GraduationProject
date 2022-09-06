<template>
	<div class="card">
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

			<v-card-title>Cineplexx Kosova</v-card-title>

			<v-divider class="mx-4"></v-divider>

			<v-card-title>Tonight's availability</v-card-title>

			<v-card-actions>
				<v-btn color="deep-purple lighten-2" text :to="{ name: 'Cineplexx' }">
					View Films
				</v-btn>
			</v-card-actions>
		</v-card>
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

			<v-card-title>ABC Cinema</v-card-title>

			<v-divider class="mx-4"></v-divider>

			<v-card-title>Tonight's availability</v-card-title>

			<v-card-actions>
				<v-btn color="deep-purple lighten-2" text :to="{ name: 'ABC' }">
					View Films
				</v-btn>
			</v-card-actions>
		</v-card>
	</div>
</template>

<script>
export default {
	name: "Cinemas",
};
</script>

<style scoped>
.card {
	flex-direction: row;
	justify-content: center;
}
</style>
