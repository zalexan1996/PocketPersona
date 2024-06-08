<template>
    <div>
        <InputText v-if="editActive" v-model="text"/>
        <span v-else>{{ text }}</span>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import InputText from 'primevue/inputtext';

const props = defineProps({
    text: {
        type: String
    },
    editActive: {
        type: Boolean
    }
})

const text = ref(props.text)
const editActive = ref(props.editActive)

</script>