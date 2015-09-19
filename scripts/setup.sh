#!/usr/bin/env bash

fail () {
  echo "$@" >&2
  exit 1
}

main () {
  update_package_list
  install_tools
  clean_up_packages
  install_bash_profile
  install_Mono_Project_Keys
  install_mono_complete
  # restart_services
}

print_section () {
  local msg="$1"
  echo "************************************************************"
  echo "$msg"
  echo "************************************************************"
}

install_tools () {
  print_section "Installing good-to-have packages"
  sudo apt-get install -y \
    build-essential \
    git-core \
    vim \
    curl \
    ack-grep \
    wget \
    tree \
    || fail "Unable to install tools."
}

update_package_list () {
  print_section "Updating package list"
  sudo apt-get update
}

install_Mono_Project_Keys () {
  print_section "Adding Mono Project Keys"
  
  # from http://www.mono-project.com/docs/getting-started/install/linux/
  sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF || \
    fail "Could not ass Mono Keys"
  sudo echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list ||\ fail "Could not add Mono repo"
  
  # needed for Ubuntu 13 and later
  sudo echo "deb http://download.mono-project.com/repo/debian wheezy-apache24-compat main" | sudo tee -a /etc/apt/sources.list.d/mono-xamarin.list || \ fail "Could not add secondary Mono repo"
  sudo apt-get update
}

install_mono_complete () {
  print_section "Installing the mono-complete package"
  sudo apt-get install -y mono-complete || fail "Unable to install mono-complete"
}

clean_up_packages () {
  print_section "Cleaning up packages"
  (sudo apt-get autoremove -y && sudo apt-get autoclean -y) || fail "Unable to clean up packages"
}

install_bash_profile () {
  print_section "Installing .bash_profile"
  cp /vagrant/configs/.bash_profile /home/vagrant/.bash_profile
  chown vagrant:vagrant /home/vagrant/.bash_profile
}

restart_services () {
  print_section "Restart services"
}

main "$@"

