import React, { useState } from 'react';
import { TextField, Button, Box, Typography, Paper } from '@mui/material';

interface CreateOrderRequestFormProps {
  onSubmit: (requestDetails: { description: string }) => void;
}

const CreateOrderRequestForm: React.FC<CreateOrderRequestFormProps> = ({ onSubmit }) => {
  const [description, setDescription] = useState<string>('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({ description });
    setDescription('');
  };

  return (
    <Paper elevation={3} sx={{ padding: 4 }}>
      <Typography variant="h5" gutterBottom>Создать Запрос на Заказ</Typography>
      <Box component="form" onSubmit={handleSubmit} sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
        <TextField
          label="Описание запроса"
          variant="outlined"
          multiline
          rows={4}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Button type="submit" variant="contained" color="primary">Создать Запрос</Button>
      </Box>
    </Paper>
  );
};

export default CreateOrderRequestForm;
