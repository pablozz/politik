import type { ReactElement } from 'react';
import Head from 'next/head';

import DefaultLayout from '@layout/DefaultLayout';

const Index = () => {
  return (
    <div>
      <Head>
        <title>Naujausi pareiškimai</title>
      </Head>
      <h1>Naujausi pareiškimai</h1>
    </div>
  );
};

Index.getLayout = (page: ReactElement) => {
  return <DefaultLayout>{page}</DefaultLayout>;
};

export default Index;
