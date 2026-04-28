import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
    plugins: [react()],
    root: './',  // pasta atual (ClientApp)
    build: {
        outDir: '../wwwroot/dist',  // ← saída para a pasta wwwroot do MVC
        emptyOutDir: true,
        manifest: true,
        rollupOptions: {
            input: './index.html',
        },
    },
    server: {
        port: 3000,
        proxy: {
            '/api': {
                target: 'https://localhost:5001',  // ← sua URL do ASP.NET
                changeOrigin: true,
                secure: false,
            },
        },
    },
});