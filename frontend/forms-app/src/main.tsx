import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import {AuthProvider} from "./components/AuthContext.tsx";

createRoot(document.getElementById('root')!).render(
    <AuthProvider>
      <StrictMode>
          <App />
      </StrictMode>
    </AuthProvider>
)
