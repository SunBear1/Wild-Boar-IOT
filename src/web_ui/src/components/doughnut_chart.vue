<template>
  <div class="chart-wrapper">
    <Doughnut :data="chartData" :style="myStyles"/>
  </div>
</template>

<script lang="ts">
import {ArcElement, Chart as ChartJS, Legend, Tooltip} from 'chart.js'
import {Doughnut} from 'vue-chartjs'

ChartJS.register(ArcElement, Tooltip, Legend)

export default {
  name: 'DoughnutChart',
  components: {Doughnut},
  computed: {
    myStyles() {
      return {
        height: '400px',
        width: '400px',
      }
    }
  },
  props: {
    chartData: {
      type: Object,
      required: true
    },
    chartOptions: {
      type: Object,
      default: () => {
      }
    }
  },
}
</script>

<style scoped>

.chart-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>
