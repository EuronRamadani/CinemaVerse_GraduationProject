<template>
	<div>
		<div class="row">
			<div class="card" style="width: 15rem">
				<img
					class="card-img-top"
					:src="event.photos[0].imgClientPath"
					alt="Card image cap"
				/>
			</div>
		</div>
	</div>
</template>

<script>
export default {
	props: {
		event: {
			type: Object,
		},
	},
	created() {
		console.log("prop", this.event);
	},
};
</script>
