<?php

$ip = "127.0.0.1";
$port = "9090";
$pem_passphrase = "<sua senha>";
$pem_file = "cert.pem";

// Informações obrigatórias para criar o certificado.
$pem_dn = array(
    "countryName" => "BR",
    "stateOrProvinceName" => "Estado",
    "localityName" => "Cidade",
    "organizationName" => "Org",
    "organizationalUnitName" => "Desenvolvimento",
    "commonName" => "127.0.0.1",
    "emailAddress" => "email@localhost"
);
