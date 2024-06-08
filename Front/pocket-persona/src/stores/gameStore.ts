import { defineStore } from "pinia";
import {ref} from 'vue';
import { GameClient, GameDto } from '../generated/api.service.ts'
import { API_BASE_URL } from "../generated/constants.ts";

export const useGameStore = defineStore('gameStore', () => {
    const client = new GameClient(API_BASE_URL)

    const games = ref<GameDto[]>()
    const selectedGame = ref<GameDto>()

    const loadGames = async () => {
        let results = await client.list();

        games.value = results.result
    }

    const deleteGame = async (id: number) => {
        await client.delete(id)
        await loadGames()
    }

    const addGame = async (name: string) => {
        await client.add({
            name: name
        })
        await loadGames()
    }

    return {
        games,
        selectedGame,
        loadGames,
        deleteGame,
        addGame
    }
})