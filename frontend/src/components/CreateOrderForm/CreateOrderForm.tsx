import React, { useState } from 'react';
import { TextField, Button, Box, Typography, Paper } from '@mui/material';

interface CreateOrderFormProps {
  onSubmit: (orderDetails: {
    description?: string;
    scheduledDate: Date;
    durationInMinutes: number;
  }) => void;
}

const CreateOrderForm: React.FC<CreateOrderFormProps> = ({ onSubmit }) => {
  const [description, setDescription] = useState<string>('');
  const [scheduledDate, setScheduledDate] = useState<Date>(new Date());
  const [durationInMinutes, setDurationInMinutes] = useState<number>(60); // Default duration

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({
      description,
      scheduledDate,
      durationInMinutes,
    });
    setDescription('');
    setScheduledDate(new Date());
    setDurationInMinutes(60);
  };

  return (
    <Paper elevation={3} sx={{ padding: 4 }}>
      <Typography variant="h5" gutterBottom>Создать Заказ</Typography>
      <Box component="form" onSubmit={handleSubmit} sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
        <TextField
          label="Описание"
          variant="outlined"
          multiline
          rows={4}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <TextField
          label="Дата и время"
          type="datetime-local"
          variant="outlined"
          value={scheduledDate.toISOString().slice(0, 16)}
          onChange={(e) => setScheduledDate(new Date(e.target.value))}
          InputLabelProps={{
            shrink: true,
          }}
        />
        <TextField
          label="Длительность (минуты)"
          type="number"
          variant="outlined"
          value={durationInMinutes}
          onChange={(e) => setDurationInMinutes(Number(e.target.value))}
        />
        <Button type="submit" variant="contained" color="primary">Создать Заказ</Button>
      </Box>
    </Paper>
  );
};

export default CreateOrderForm;
