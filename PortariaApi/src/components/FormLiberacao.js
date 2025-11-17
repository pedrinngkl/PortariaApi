import React, { useState } from 'react';
import axios from 'axios';
const API_URL = 'https://localhost:7144'; 

export function FormLiberacao() {
  const [nomeVisitante, setNomeVisitante] = useState('');
  const [documentoVisitante, setDocumentoVisitante] = useState('');
  const [mensagem, setMensagem] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    setMensagem('Processando...');

    try {
      // --- PASSO 1: Criar ou encontrar o Visitante ---
      // Envia os dados para /api/Visitantes
      const responseVisitante = await axios.post(`${API_URL}/api/Visitantes`, {
        nomeCompleto: nomeVisitante,
        documento: documentoVisitante
      });

      // Pega o ID do visitante que a API retornou
      const visitanteId = responseVisitante.data.visitanteID;
      console.log('Visitante ID:', visitanteId);

      if (!visitanteId) {
        throw new Error('Não foi possível obter o ID do visitante.');
      }

      // --- PASSO 2: Criar a Liberação ---
      // (Por agora, vamos "chumbar" (hardcode) os IDs do morador e apto)
      const dadosLiberacao = {
        dataPrevista: new Date(), // Pega a data/hora de agora
        status: 0, // 0 = Pendente (nosso Enum C#)
        fk_MoradorID: 1, // <-- MUDAR (ID de um morador que exista no seu banco)
        fk_VisitanteID: visitanteId, // O ID que conseguimos acima
        fk_ApartamentoID: 1 // <-- MUDAR (ID de um apartamento que exista)
      };

      // Envia os dados para /api/Liberacoes
      const responseLiberacao = await axios.post(`${API_URL}/api/Liberacoes`, dadosLiberacao);

      console.log('Liberação criada:', responseLiberacao.data);
      setMensagem('Liberação criada com sucesso!');
      setNomeVisitante('');
      setDocumentoVisitante('');

    } catch (error) {
      console.error('Erro ao criar liberação:', error);
      setMensagem(`Erro: ${error.message}`);
    }
  };

  return (
    <div style={{ padding: '20px', maxWidth: '400px', margin: 'auto' }}>
      <h2>Liberar Novo Visitante</h2>
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '10px' }}>
          <label>
            Nome do Visitante:
            <input
              type="text"
              value={nomeVisitante}
              onChange={(e) => setNomeVisitante(e.target.value)}
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </label>
        </div>
        <div style={{ marginBottom: '10px' }}>
          <label>
            Documento (CPF/RG):
            <input
              type="text"
              value={documentoVisitante}
              onChange={(e) => setDocumentoVisitante(e.target.value)}
              required
              style={{ width: '100%', padding: '8px' }}
            />
          </label>
        </div>
        <button type="submit" style={{ padding: '10px 15px' }}>
          Autorizar
        </button>
      </form>
      {mensagem && <p>{mensagem}</p>}
    </div>
  );
}