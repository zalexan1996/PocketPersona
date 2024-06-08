<template>
    <nav class="navbar navbar-dark bg-dark navbar-expand-lg">
        <div class="container-fluid">
            <a class="navbar-brand">Pocket Persona</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarToggler">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarToggler">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item me-2"><RouterLink :to="{name: 'arcana'}"><Button>Arcana</Button></RouterLink></li>
                    <li class="nav-item me-2"><RouterLink :to="{name: 'characters'}"><Button>Characters</Button></RouterLink></li>
                    <li class="nav-item me-2"><RouterLink :to="{name: 'social-links'}"><Button>Social Links</Button></RouterLink></li>
                    <li class="nav-item me-2"><RouterLink :to="{name: 'games'}"><Button>Games</Button></RouterLink></li>
                </ul>
                <li class="nav-item me-2">
                    <DropDown placeholder="Choose a game..." class="w-full" :options="gameStore.games" v-model="gameStore.selectedGame">
                        <template #value="slotProps">
                            <span v-if="slotProps.value">{{ slotProps.value.name }}</span>
                            <span v-else>{{ slotProps.placeholder }}</span>
                        </template>
                        <template #option="slotProps">
                            {{ slotProps.option.name }}
                        </template>
                    </DropDown>
                </li>
            </div>
        </div>
    </nav>
    <div>
        <RouterView></RouterView>
    </div>
</template>
<script setup>
import Button from 'primevue/button';
import DropDown from 'primevue/dropdown';
import { onMounted } from 'vue';
import { RouterLink, RouterView } from 'vue-router';
import { useGameStore } from '../stores/gameStore';

const gameStore = useGameStore();

onMounted(async () => {
    await gameStore.loadGames();
})
</script>