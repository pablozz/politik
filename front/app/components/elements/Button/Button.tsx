import { ReactNode } from 'react';
import cx from 'classnames';

interface IButton extends React.HTMLAttributes<HTMLElement> {
  icon?: ReactNode;
  variant?: 'contained' | 'outlined';
  type?: 'submit' | 'button';
  form?: string;
  disabled?: boolean;
}

const Button: React.FC<IButton> = ({
  icon,
  variant = 'contained',
  children,
  type = 'button',
  disabled,
  ...rest
}) => {
  const classNames = cx(
    'text-white',
    'font-bold',
    'py-2',
    'px-4',
    'rounded',
    'inline-flex',
    'items-center',
    'space-x-4',
    'border-primary',
    'hover:bg-blue-100',
    'hover:border-primary-darker',
    {
      'text-white': variant === 'contained',
      'text-primary': variant === 'outlined',
      'bg-primary': variant === 'contained',
      'bg-transparent': variant === 'outlined',
      'hover:bg-primary-darker': variant === 'contained',
      'hover:bg-primary-background': variant === 'outlined',
      'hover:text-white': variant === 'contained',
      border: variant === 'outlined',
      'bg-primary-light': disabled,
      'hover:bg-primary-light': disabled,
      'cursor-not-allowed': disabled,
    }
  );

  return (
    <button className={classNames} type={type} disabled={disabled} {...rest}>
      {!!icon && <span className="mr-2">{icon}</span>}
      {children}
    </button>
  );
};

export default Button;