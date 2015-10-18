<?php

require_once 'conf.php';

// Criando chave privada
$privkey = openssl_pkey_new();

// Criar e assinar CSR
$cert = openssl_csr_new($pem_dn, $privkey);
$cert = openssl_csr_sign($cert, null, $privkey, 365);

// Criando arquivo .pem
$pem = array();
openssl_x509_export($cert, $pem[0]);
openssl_pkey_export($privkey, $pem[1], $pem_passphrase);
$pem = implode($pem);

// Salvando arquivo .pem
file_put_contents($pem_file, $pem);
chmod($pem_file, 0600);

echo "Certificado criado: $pem_file\n";
