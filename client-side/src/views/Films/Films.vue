<template>
	<v-app>
		<v-main class="grey lighten-3">
			<v-container>
				<v-row>
					<template v-for="n in 4">
						<v-col :key="n" class="" cols="12">
							<strong>Category {{ n }}</strong>
						</v-col>

						<v-col v-for="j in 6" :key="`${n}${j}`" cols="5" md="2">
							<v-sheet height="150">Filmi Hachico</v-sheet>
						</v-col>
					</template>
				</v-row>
			</v-container>
		</v-main>
	</v-app>
</template>

<script>
export default {
	data: () => ({ drawer: null }),
};
</script>
