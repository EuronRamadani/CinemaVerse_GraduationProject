<template>
	<div>
		<!-- <h1>Homepage</h1> -->
		<UpcomingMovies />
	</div>
</template>

<script>
import UpcomingMovies from "../components/SlideshowMovies.vue";

export default {
	name: "Home",
	components: {
		UpcomingMovies,
	},
};
</script>
