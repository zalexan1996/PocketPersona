<template>
    <div class="d-flex flex-row justify-content-between">
        <h3>Games</h3>
        <div>
            <InputText v-model="newGameName" placeholder="Enter a game name..." type="text"/>
            <Button severity="success" @click="gameStore.addGame(newGameName)"><font-awesome-icon icon="fa-solid fa-plus" class="me-2"/>Add</Button>
        </div>
    </div>
    <DataTable :value="gameStore.games">
        <Column field="id" header="Id"/>
        <Column field="name" header="Name">
            <template #body="slotProps">
                <EditableInputText :text="slotProps.data.name" :editActive="false" />
            </template>
        </Column>
        <Column header="Actions">
            <template #body="slotProps">
                <div class="flex flex-row">
                    <Button severity="danger" class="me-2" @click="gameStore.deleteGame(slotProps.data.id)"><font-awesome-icon icon="fa-solid fa-trash" class="me-2"/>Delete</Button>
                    <Button severity="warning" class="me-2"><font-awesome-icon icon="fa-solid fa-edit" class="me-2"/>Edit</Button>
                </div>
            </template>
        </Column>
    </DataTable>
</template>
<script setup>
import {ref} from 'vue';
import { useGameStore } from '@/stores/gameStore';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import InputText from 'primevue/inputText';
import EditableInputText from '@/components/EditableInputText.vue';

const gameStore = useGameStore()
const newGameName = ref('')

</script>