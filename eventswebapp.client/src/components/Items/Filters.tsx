import { NavLink } from 'react-router-dom';
import { AppliedFilters, SocialEventModel } from '../../types/types';
import { useForm } from 'react-hook-form';

export default function Items(props: Props) {
  const { register, handleSubmit } = useForm<AppliedFilters>();

  function onSubmitFilters(data: AppliedFilters) {
    const formData = new FormData();
    formData.append('name', data.name);
    formData.append('category', data.category);
    formData.append('date', data.date.toString());
    formData.append('place', data.place);

    console.log(formData);
    CreateEvent(formData)
      .then((res) => {
        navigate('/eventPage', { state: { ...res } });
      })
      .catch();
  }
  return <div></div>;
}
