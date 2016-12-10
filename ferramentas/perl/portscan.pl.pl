#!/usr/bin/perl

#### OPEN MODULOS ####
use strict;
use warnings;
use IO::Socket::INET;
#### /END MODULOS ####

#### OPEN VARIAVEIS ####
my $target = $ARGV[0];
my $porta1 = $ARGV[1];
my $porta2 = $ARGV[2];
my $protoc = $ARGV[3];
#### /END VARIAVEIS ####

# Regex para remover https, http e www.
$target =~ s/https:\/\/// || $target =~ s/http:\/\/// || $target =~ s/www.// ;

while ( $porta1 <= $porta2 ) {
	
	my $socket = new IO::Socket::INET (
		Peeraddr => $target,
		PeerPort => $porta1,
		Proto    => $protoc,
		Timeout  => "5");

	if ($socket) {
		print "\n[+] porta $porta1 aberta.\n";
	}

	$porta1++;
}